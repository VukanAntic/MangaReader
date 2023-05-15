using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using UserInfo.API.Entities;

namespace UserInfo.API.Repository
{
    public class UserInfoReposotory : IUserInformationRepository
    {
        private readonly IDistributedCache _context;

        public UserInfoReposotory(IDistributedCache context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserInformation> CreateUserInfo(string id)
        {
            var userInfo = await _context.GetStringAsync(id);
            if (!string.IsNullOrEmpty(userInfo))
            {
                return null;
            }

            var newUserInfo = JsonConvert.SerializeObject(new UserInformation(id));
            await _context.SetStringAsync(id, newUserInfo);

            return await GetUserInfo(id);
        }

        public async Task<UserInformation> GetUserInfo(string id)
        {
            var userInfo = await _context.GetStringAsync(id);
            if(string.IsNullOrEmpty(userInfo))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UserInformation>(userInfo);
        }

        //public async Task<UserInformation> UpdateUserInfo(string id)
        //{
        //    var userInfo = await _context.GetStringAsync(id);
        //    if (string.IsNullOrEmpty(userInfo))
        //    {
        //        return null;
        //    }
        //    //var userInfoString = JsonConvert.SerializeObject(userInformation);
        //    //await _context.SetStringAsync(userInformation.ID, userInfoString);

        //    return await GetUserInfo(id);
        //}
        public async Task<bool> DeleteUserInfo(string id)
        {
            var userInfo = await _context.GetStringAsync(id);
            if (string.IsNullOrEmpty(userInfo))
            {
                return false;
            }
            await _context.RemoveAsync(id);
            return true;
        }

        public async Task<UserInformation> UpdateLastReadManga(string userId, string lastReadMangaId)
        {
            var userInfo = await _context.GetStringAsync(userId);
            if (string.IsNullOrEmpty(userInfo))
            {
                return null;
            }

            var deserializedUser = JsonConvert.DeserializeObject<UserInformation>(userInfo);
            deserializedUser.ReadingJournal.LastReadMangaID = lastReadMangaId;

            await _context.SetStringAsync(userId, JsonConvert.SerializeObject(deserializedUser));

            return await GetUserInfo(userId);

        }

        public async Task<UserInformation> AddMangaInAllReadMangaIds(string userId, string newManga)
        {
            var userInfo = await _context.GetStringAsync(userId);
            if (string.IsNullOrEmpty(userInfo))
            {
                return null;
            }

            var deserializedUser = JsonConvert.DeserializeObject<UserInformation>(userInfo);
            var allReadMangaList = deserializedUser.ReadingJournal.AllReadMangaIDs;
            int index = allReadMangaList.LastIndexOf(newManga);

            if (index == -1)
            {
                allReadMangaList.Add(newManga);
                await _context.SetStringAsync(userId, JsonConvert.SerializeObject(deserializedUser));

                return await GetUserInfo(userId);
            }

            return await GetUserInfo(userId);
        }

        public async Task<UserInformation> AddMangaInWishlist(string userId, string newManga)
        {
            var userInfo = await _context.GetStringAsync(userId);
            if (string.IsNullOrEmpty(userInfo))
            {
                return null;
            }

            var deserializedUser = JsonConvert.DeserializeObject<UserInformation>(userInfo);
            var whishlist = deserializedUser.ReadingJournal.WishList;
            int index = whishlist.LastIndexOf(newManga);

            if (index == -1)
            {
                whishlist.Add(newManga);
                await _context.SetStringAsync(userId, JsonConvert.SerializeObject(deserializedUser));

                return await GetUserInfo(userId);
            }

            return await GetUserInfo(userId);
        }

        public async Task<UserInformation> RemoveMangaFromWishlist(string userId, string newManga)
        {
            var userInfo = await _context.GetStringAsync(userId);
            if (string.IsNullOrEmpty(userInfo))
            {
                return null;
            }

            var deserializedUser = JsonConvert.DeserializeObject<UserInformation>(userInfo);
            var whishlist = deserializedUser.ReadingJournal.WishList;
            int index = whishlist.LastIndexOf(newManga);

            if (index == -1)
            {
                return await GetUserInfo(userId);
            }

            whishlist.Remove(newManga);
            await _context.SetStringAsync(userId, JsonConvert.SerializeObject(deserializedUser));

            return await GetUserInfo(userId);
        }
    }
}

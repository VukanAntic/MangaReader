using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using UserInfo.Common.DTOs;
using UserInfo.Common.Entities;

namespace UserInfo.Common.Repository
{
    public class UserInfoReposotory : IUserInformationRepository
    {
        private readonly IDistributedCache _context;
        private readonly IMapper _mapper;

        public UserInfoReposotory(IDistributedCache context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserInfoDTO> CreateUserInfo(CreateUserInfoDTO userInfoDTO)
        {
            var userInfo = await _context.GetStringAsync(userInfoDTO.userId);
            if (!string.IsNullOrEmpty(userInfo))
            {
                return null;
            }

            var newUserInfo = JsonConvert.SerializeObject(new UserInformation(userInfoDTO.userId));
            await _context.SetStringAsync(userInfoDTO.userId, newUserInfo);

            return await GetUserInfo(userInfoDTO.userId);
        }

        public async Task<UserInfoDTO> GetUserInfo(string id)
        {
            var userInfo = await _context.GetStringAsync(id);
            if(string.IsNullOrEmpty(userInfo))
            {
                return null;
            }

            var userInfoResult = JsonConvert.DeserializeObject<UserInfoDTO>(userInfo);
            return _mapper.Map<UserInfoDTO>(userInfoResult);
            
        }

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


        public async Task<UserInfoDTO> UpdateLastReadManga(UpdateUserInfoDTO updateUserInfo)
        {
            var userInfo = await _context.GetStringAsync(updateUserInfo.userId);
            if (string.IsNullOrEmpty(userInfo))
            {
                return null;
            }

            var deserializedUser = JsonConvert.DeserializeObject<UserInfoDTO>(userInfo);
            deserializedUser.ReadingJournal.LastReadMangaID = updateUserInfo.mangaId;

            await _context.SetStringAsync(updateUserInfo.userId, JsonConvert.SerializeObject(deserializedUser));

            return await GetUserInfo(updateUserInfo.userId);

        }

        public async Task<UserInfoDTO> AddMangaInAllReadMangaIds(UpdateUserInfoDTO userInfoDTO)
        {
            var userInfo = await _context.GetStringAsync(userInfoDTO.userId);
            if (string.IsNullOrEmpty(userInfo))
            {
                return null;
            }

            var deserializedUser = JsonConvert.DeserializeObject<UserInfoDTO>(userInfo);
            var allReadMangaList = deserializedUser.ReadingJournal.AllReadMangaIDs;
            var lastReadManga = deserializedUser.ReadingJournal.LastReadMangaID;    // dodato
            int index = allReadMangaList.LastIndexOf(userInfoDTO.mangaId);

            if (index == -1)
            {
                allReadMangaList.Add(userInfoDTO.mangaId);

                await _context.SetStringAsync(userInfoDTO.userId, JsonConvert.SerializeObject(deserializedUser));

                return await UpdateLastReadManga(userInfoDTO);
                //return await GetUserInfo(userInfoDTO.userId);
            }

            return await GetUserInfo(userInfoDTO.userId);
        }

        public async Task<UserInfoDTO> AddMangaInWishlist(UpdateUserInfoDTO userInfoDTO)
        {
            var userInfo = await _context.GetStringAsync(userInfoDTO.userId);
            if (string.IsNullOrEmpty(userInfo))
            {
                return null;
            }

            var deserializedUser = JsonConvert.DeserializeObject<UserInformation>(userInfo);
            var whishlist = deserializedUser.ReadingJournal.WishList;
            int index = whishlist.LastIndexOf(userInfoDTO.mangaId);

            if (index == -1)
            {
                whishlist.Add(userInfoDTO.mangaId);
                await _context.SetStringAsync(userInfoDTO.userId, JsonConvert.SerializeObject(deserializedUser));

                return await GetUserInfo(userInfoDTO.userId);
            }

            return await GetUserInfo(userInfoDTO.userId);
        }

        public async Task<UserInfoDTO> RemoveMangaFromWishlist(UpdateUserInfoDTO userInfoDTO)
        {
            var userInfo = await _context.GetStringAsync(userInfoDTO.userId);
            if (string.IsNullOrEmpty(userInfo))
            {
                return null;
            }

            var deserializedUser = JsonConvert.DeserializeObject<UserInformation>(userInfo);
            var whishlist = deserializedUser.ReadingJournal.WishList;
            int index = whishlist.LastIndexOf(userInfoDTO.mangaId);

            if (index == -1)
            {
                return await GetUserInfo(userInfoDTO.userId);
            }

            whishlist.Remove(userInfoDTO.mangaId);
            await _context.SetStringAsync(userInfoDTO.userId, JsonConvert.SerializeObject(deserializedUser));

            return await GetUserInfo(userInfoDTO.userId);
        }
    }
}

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

        public async Task<UserInformation> GetUserInfo(string id)
        {
            var userInfo = await _context.GetStringAsync(id);
            if(string.IsNullOrEmpty(userInfo))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UserInformation>(userInfo);
        }
        public async Task<UserInformation> UpdateUserInfo(UserInformation userInformation)
        {
            var userInfoString = JsonConvert.SerializeObject(userInformation);
            await _context.SetStringAsync(userInformation.ID, userInfoString);

            return await GetUserInfo(userInformation.ID);
        }
        public async Task DeleteUserInfo(string id)
        {
            await _context.RemoveAsync(id);
        }
    }
}

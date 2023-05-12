using UserInfo.API.Entities;

namespace UserInfo.API.Repository
{
    public interface IUserInformationRepository
    {
        Task<UserInformation> GetUserInfo(string id);
        Task<UserInformation> UpdateUserInfo(UserInformation userInformation);
        Task DeleteUserInfo(string id);
    }
}

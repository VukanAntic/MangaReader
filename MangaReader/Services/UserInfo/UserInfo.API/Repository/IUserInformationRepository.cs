using UserInfo.API.Entities;

namespace UserInfo.API.Repository
{
    public interface IUserInformationRepository
    {
        Task<UserInformation> CreateUserInfo(string id);
        Task<UserInformation> GetUserInfo(string id);
        Task<bool> DeleteUserInfo(string id);

        Task<UserInformation> UpdateLastReadManga(string userId, string lastReadMangaId);
        Task<UserInformation> AddMangaInAllReadMangaIds(string userId, string newManga);
        Task<UserInformation> AddMangaInWishlist(string userId, string newManga);
        Task<UserInformation> RemoveMangaFromWishlist(string userId, string newManga);
    }
}

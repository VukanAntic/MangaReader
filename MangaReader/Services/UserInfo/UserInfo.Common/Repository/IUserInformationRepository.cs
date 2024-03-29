﻿using UserInfo.Common.DTOs;
using UserInfo.Common.Entities;

namespace UserInfo.Common.Repository
{
    public interface IUserInformationRepository
    {
        Task<UserInfoDTO> CreateUserInfo(CreateUserInfoDTO userInfoDTO);
        Task<UserInfoDTO> GetUserInfo(string id);
        Task<bool> DeleteUserInfo(string id);

        Task<UserInfoDTO> UpdateLastReadManga(UpdateUserInfoDTO userInfoDTO);
        Task<UserInfoDTO> AddMangaInAllReadMangaIds(UpdateUserInfoDTO userInfoDTO);
        Task<UserInfoDTO> AddMangaInWishlist(UpdateUserInfoDTO userInfoDTO);
        Task<UserInfoDTO> RemoveMangaFromWishlist(UpdateUserInfoDTO userInfoDTO);
    }
}

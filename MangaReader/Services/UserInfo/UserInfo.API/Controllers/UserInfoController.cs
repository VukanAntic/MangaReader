﻿using Microsoft.AspNetCore.Mvc;
using UserInfo.API.Entities;
using UserInfo.API.Repository;
using UserInfo.API.DTOs;

namespace UserInfo.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInformationRepository _repository;

        public UserInfoController(IUserInformationRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserInfoDTO>> CreateUser([FromBody] CreateUserInfoDTO userInfoDTO) 
        {
            var userInfo = await _repository.CreateUserInfo(userInfoDTO);
            if(userInfo is null)
            {
                return Conflict(userInfo);
            }
            return Ok(userInfo);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInfoDTO>> GetUserInfo(string id) 
        {
            var userInfo = await _repository.GetUserInfo(id);
            if(userInfo is null)
            {
                return NotFound();
            }
            return Ok(userInfo);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInfoDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserInfo(string id)
        {
            var result = await _repository.DeleteUserInfo(id);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInfoDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInfoDTO>> UpdateLastReadManga(UpdateUserInfoDTO userInfoDTO) 
        {
            var updatedUserInfo = await _repository.UpdateLastReadManga(userInfoDTO);
            if (updatedUserInfo is null)
            {
                return NotFound(updatedUserInfo);
            }

            return Ok(updatedUserInfo);
        }


        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInfoDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInfoDTO>> AddMangaInAllReadMangaIds(UpdateUserInfoDTO userInfoDTO) 
        {
            var updatedUserInfo = await _repository.AddMangaInAllReadMangaIds(userInfoDTO);
            if (updatedUserInfo is null)
            {
                return NotFound(updatedUserInfo);
            }
            return Ok(updatedUserInfo);
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInfoDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInfoDTO>> AddMangaInWishlist(UpdateUserInfoDTO userInfoDTO) 
        {
            var updatedUserInfo = await _repository.AddMangaInWishlist(userInfoDTO);
            if (updatedUserInfo is null)
            {
                return NotFound(updatedUserInfo);
            }

            return Ok(updatedUserInfo);
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInfoDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInfoDTO>> RemoveMangaFromWishlist(UpdateUserInfoDTO userInfoDTO) 
        {
            var updatedUserInfo = await _repository.RemoveMangaFromWishlist(userInfoDTO);
            if (updatedUserInfo is null)
            {
                return NotFound(updatedUserInfo);
            }
            return Ok(updatedUserInfo);
        }
    }
}
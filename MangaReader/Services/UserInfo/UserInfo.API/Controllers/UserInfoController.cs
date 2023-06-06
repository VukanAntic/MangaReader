using Microsoft.AspNetCore.Mvc;
using UserInfo.Common.Entities;
using UserInfo.Common.Repository;
using UserInfo.Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Forms;

namespace UserInfo.Common.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInformationRepository _repository;
        private readonly ILogger<UserInfoController> _logger;

        public UserInfoController(IUserInformationRepository repository, ILogger<UserInfoController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserInfoDTO>> CreateUser() 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userInfo = await _repository.CreateUserInfo(new CreateUserInfoDTO(userId));
            if(userInfo is null)
            {
                return Conflict(userInfo);
            }
            return Ok(userInfo);
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInfoDTO>> GetUserInfo() 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userInfo = await _repository.GetUserInfo(userId);
            if(userInfo is null)
            {
                return NotFound();
            }
            return Ok(userInfo);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInfoDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await _repository.DeleteUserInfo(userId);
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
        public async Task<ActionResult<UserInfoDTO>> UpdateLastReadManga([FromBody] InputData inputData) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedUserInfo = await _repository.UpdateLastReadManga(new UpdateUserInfoDTO(userId, inputData.mangaId));

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
        public async Task<ActionResult<UserInfoDTO>> AddMangaInAllReadMangaIds([FromBody] InputData inputData) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedUserInfo = await _repository.AddMangaInAllReadMangaIds(new UpdateUserInfoDTO(userId, inputData.mangaId));

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
        public async Task<ActionResult<UserInfoDTO>> AddMangaInWishlist([FromBody] InputData inputData) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedUserInfo = await _repository.AddMangaInWishlist(new UpdateUserInfoDTO(userId, inputData.mangaId));

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
        public async Task<ActionResult<UserInfoDTO>> RemoveMangaFromWishlist([FromBody] InputData inputData) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedUserInfo = await _repository.RemoveMangaFromWishlist(new UpdateUserInfoDTO(userId, inputData.mangaId));

            if (updatedUserInfo is null)
            {
                return NotFound(updatedUserInfo);
            }
            return Ok(updatedUserInfo);
        }
    }
}

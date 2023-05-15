using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserInformation>> CreateUser([FromBody] string userId) // createUserDTO dodati
        {
            var userInfo = await _repository.CreateUserInfo(userId);
            if(userInfo is null)
            {
                return Conflict(userInfo);
            }
            return Ok(userInfo);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInformation>> GetUserInfo(string id) 
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
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserInfo(string id)
        {
            var result = await _repository.DeleteUserInfo(id);
            if(result)
            {
                return Ok();
            }
            return NotFound();
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInformation>> UpdateLastReadManga(string userId, string lastReadMangaId) // dodati UpdateMangaDTO
        {
            var updatedUserInfo = await _repository.UpdateLastReadManga(userId, lastReadMangaId);
            if (updatedUserInfo is null)
            {
                return NotFound(updatedUserInfo);
            }

            return Ok(updatedUserInfo);
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInformation>> AddMangaInAllReadMangaIds(string userId, string newManga) // dodati UpdateMangaDTO
        {
            var updatedUserInfo = await _repository.AddMangaInAllReadMangaIds(userId, newManga);
            if(updatedUserInfo is null)
            {
                return NotFound(updatedUserInfo);
            }
            return Ok(updatedUserInfo);
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInformation>> AddMangaInWishlist(string userId, string newManga) // dodati UpdateMangaDTO
        {
            var updatedUserInfo = await _repository.AddMangaInWishlist(userId, newManga);
            if (updatedUserInfo is null)
            {
                return NotFound(updatedUserInfo);
            }
            return Ok(updatedUserInfo);
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInformation>> RemoveMangaFromWishlist(string userId, string newManga) // dodati UpdateMangaDTO
        {
            var updatedUserInfo = await _repository.RemoveMangaFromWishlist(userId, newManga);
            if (updatedUserInfo is null)
            {
                return NotFound(updatedUserInfo);
            }
            return Ok(updatedUserInfo);
        }
    }
}

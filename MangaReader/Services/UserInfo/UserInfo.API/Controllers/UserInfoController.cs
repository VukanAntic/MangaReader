using Microsoft.AspNetCore.Mvc;
using UserInfo.API.Entities;
using UserInfo.API.Repository;

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


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserInformation>> GetUserInfo(string id) 
        {
            var userInfo = await _repository.GetUserInfo(id);
            return Ok(userInfo ?? new UserInformation(id));
        }


        [HttpPut]
        [ProducesResponseType(typeof(UserInformation), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserInformation>> UpdateUserInfo([FromBody] UserInformation userInformation)
        {
            return Ok(await _repository.UpdateUserInfo(userInformation));
        }


        [HttpDelete]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUserInfo(string id)
        {
            await _repository.DeleteUserInfo(id);
            return Ok();
        }



    }
}

 using System;
using System.Security.Claims;
using AutoMapper;
using IdentityServer.DTOs;
using IdentityServer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, ILogger<UserController> logger, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserDetailsDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetailsDTO>> GetUser()
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(username);

            return Ok(_mapper.Map<UserDetailsDTO>(user));
        }


        [HttpDelete]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteUserAccount()
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(username);
            await _userManager.DeleteAsync(user);

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> ChangePassword([FromBody] UserChangePasswordDTO changepassword)
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(username);
           
            return Ok(await _userManager.ChangePasswordAsync(user, changepassword.OldPassword, changepassword.NewPassword));
        }


        [HttpPut("[action]")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> ChangeEmail([FromBody] UserChangeEmailDTO changeEmail)
        {
            var name = User.FindFirst(ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(name);

            await _userManager.SetEmailAsync(user, changeEmail.NewEmail);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return Ok(await _userManager.ConfirmEmailAsync(user, token));

        }

    }
}












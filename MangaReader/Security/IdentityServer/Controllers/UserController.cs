 using System;
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
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(UserDetailsDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetailsDTO>> GetUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return Ok(_mapper.Map<UserDetailsDTO>(user));
        }

       
        //public async Task<ActionResult> DeleteUserAccount(string username)
        //{

        //}

        //public async Task<ActionResult> ChangePassword([FromBody] ChangePassword changepassword)
        //{

        //}
    }
}

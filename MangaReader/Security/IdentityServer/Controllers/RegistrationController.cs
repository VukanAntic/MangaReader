 using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using IdentityServer.DTOs;
using AutoMapper;
using IdentityServer.Controllers.Base;
using System.Data;
using MassTransit;

namespace IdentityServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : RegistratonControllerBase
    {
        public AuthenticationController(ILogger<AuthenticationController> logger, UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper, IPublishEndpoint publishEndpoint) 
            : base(logger, userManager, roleManager, mapper, publishEndpoint)
        {
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] UserCreateDTO user)
        {

            return await RegisterNewUserWithRoles(user, new string[] { "User" });

        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAdministrator([FromBody] UserCreateDTO user)
        {

            return await RegisterNewUserWithRoles(user, new string[] { "Administrator" });

        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateDTO role)
        {

            var newRole = _mapper.Map<Role>(role);

            if (ModelState.IsValid)
            {

                IdentityResult result = await _roleManager.CreateAsync(newRole);

                if (result.Succeeded)
                {
                    
                    return StatusCode(StatusCodes.Status201Created);
                  
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
    }
}

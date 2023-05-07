using AutoMapper;
using IdentityServer.DTOs;
using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers.Base
{
    public class RegisterControllerBase : ControllerBase
    {
        protected readonly UserManager<User> _userManager;
        protected readonly RoleManager<Role> _roleManager;
        protected readonly IMapper _mapper;

        public RegisterControllerBase(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected async Task<IActionResult> RegisterNewUserWithRoles(UserCreateDTO user, string[] roles)
        {

            var newUser = _mapper.Map<User>(user);

            if (ModelState.IsValid)
            {

                IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);

                if (result.Succeeded)
                {
                    foreach (var role in roles)
                    {
                        var roleExists = await _roleManager.RoleExistsAsync(role);
                        if (roleExists)
                        {
                            await _userManager.AddToRoleAsync(newUser, role);

                        }
                        else
                        {
                            // Ne smatra se greskom!
                        }

                        return StatusCode(StatusCodes.Status201Created);
                    }
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

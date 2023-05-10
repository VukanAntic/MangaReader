using System;
using IdentityServer.DTOs;
using IdentityServer.AuthenticationServices;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
	{
        private readonly IAuthenticationService _authService;
        protected readonly ILogger<AuthenticationController> _logger;

        public LoginController(IAuthenticationService authService, ILogger<AuthenticationController> logger)
        
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthenticationModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDTO userCredentials)
        {
            var user = await _authService.ValidateUser(userCredentials);
            if (user is null)
            {
                _logger.LogWarning("{Login}: Authentication failed. Wrong email or password.", nameof(Login));
                return Unauthorized();
            }

            return Ok(await _authService.CreateAuthenticationModel(user));
        }
    }
}


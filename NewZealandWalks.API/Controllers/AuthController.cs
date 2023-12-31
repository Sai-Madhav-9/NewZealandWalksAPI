﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Models.DTO;
using NewZealandWalks.API.Repositories;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser 
            { 
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var IdentityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if(IdentityResult.Succeeded)
            {
                if (registerRequestDto.Roles!=null && registerRequestDto.Roles.Any())
                {
                   IdentityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if(IdentityResult.Succeeded)
                    {
                        return Ok("User was registered! Your can login");
                    }
                }
            }

            return BadRequest("Something went wrong sorry");


        }


        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> LoginUserMethod([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var result = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (result)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var rolesList = roles.ToList();
                        var jwtToken =  tokenRepository.CreateJWTToken(user, rolesList);
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };


                        return Ok(response);
                    }
                    
                }
            }
            return BadRequest("Username or Password is incorrect");
        }
    }


}

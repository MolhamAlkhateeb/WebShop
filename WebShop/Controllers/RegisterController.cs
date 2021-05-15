using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data;
using WebShop.Models;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAuthenticationService authService;

        public RegisterController(UserManager<ApplicationUser> userManager,IAuthenticationService authService)
        {
            this.userManager = userManager;
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = new ApplicationUser { 
                Email = registerDto.Email ,
                UserName = registerDto.Email
            };
            var created = await userManager.CreateAsync(user,registerDto.Password);
            if (!created.Succeeded)
            {
                return BadRequest();
            }
            var roles = new List<string>();
            var token = authService.GenerateToken(user, roles);
            var refreshToken = authService.GenerateRefreshToken(token);
            user.RefreshToken = refreshToken;
            var isUpdated = await userManager.UpdateAsync(user);
            if (!isUpdated.Succeeded)
            {
                return StatusCode(500);
            }

            var response = new TokenWithRefresh()
            {
                AccessToken = token,
                RefreshToken = refreshToken.Token
            };
            return Ok(response);
        }
    }
}

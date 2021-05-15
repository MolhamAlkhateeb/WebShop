using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data;
using WebShop.Models;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAuthenticationService authService;

        public AuthenticateController(UserManager<ApplicationUser> userManager, IAuthenticationService authService)
        {
            this.userManager = userManager;
            this.authService = authService;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto  loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.Username);
            var userRoles = await userManager.GetRolesAsync(user);
            var isCorrectPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isCorrectPassword)
            {
                return BadRequest();
            }
            var token = authService.GenerateToken(user, userRoles);
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

        [Route("accesstoken")]
        [HttpPost]
        public async Task<IActionResult> GetAccessToken([FromBody] TokenWithRefresh tokens)
        {
            var principal = authService.GetClaimsFromToken(tokens.AccessToken, false);
            var userId = principal.Claims.SingleOrDefault(x => x.Type == nameof(ApplicationUser.Id)).Value;
            var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var user = await userManager.FindByIdAsync(userId);
            var userRoles = await userManager.GetRolesAsync(user);
            var isValid = (tokens.RefreshToken == user.RefreshToken.Token) &&
                (!user.RefreshToken.IsRevoked) &&
                (!user.RefreshToken.IsExpired);
            //&&(user.RefreshToken.Jti == jti);
            if (!isValid)
            {
                return Unauthorized();
            }
            var newAccessToken = authService.GenerateToken(user, userRoles);
            return Ok(newAccessToken);
        }
    }
}

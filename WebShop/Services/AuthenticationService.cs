using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebShop.Data;
using WebShop.Models;
using WebShop.Services.Interfaces;

namespace WebShop.Services
{

    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtConfig jwtConfig;

        public AuthenticationService(JwtConfig jwtConfig)
        {
            this.jwtConfig = jwtConfig;
        }

        public string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var jwtSecterKey = Encoding.ASCII.GetBytes(jwtConfig.SecretKey);
            var securityKey = new SymmetricSecurityKey(jwtSecterKey);
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });
            foreach (var role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(jwtConfig.TokenExpiryInMinutes),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var accessToken = jwtTokenHandler.WriteToken(token);
            return accessToken;
        }

        public ClaimsPrincipal GetClaimsFromToken(string token, bool validateExpiry = true)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateLifetime = validateExpiry
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals("HS512", StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public RefreshToken GenerateRefreshToken(string token)
        {
            var principal = GetClaimsFromToken(token, false);
            var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var userId = principal.Claims.SingleOrDefault(x => x.Type == nameof(ApplicationUser.Id)).Value;
            var refreshToken = new RefreshToken()
            {
                UserId = userId,
                Jti = jti,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMonths(jwtConfig.RefreshTokenExpiryInMonths),
                Token = GenerateRandomString() + Guid.NewGuid()
            };
            return refreshToken;
        }

        public string GenerateRandomString(int length = 64)
        {
            var random = new byte[length];
            using var randomGenerator = RandomNumberGenerator.Create();
            randomGenerator.GetBytes(random);
            return Convert.ToBase64String(random);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Services.Interfaces
{
    public interface IAuthenticationService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);

        ClaimsPrincipal GetClaimsFromToken(string token, bool validateExpiry);

        string GenerateRandomString(int length);

        RefreshToken GenerateRefreshToken(string token);
    }
}

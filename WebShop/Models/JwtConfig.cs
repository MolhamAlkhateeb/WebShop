using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class JwtConfig
    {
        public string SecretKey { get; set; }
        public int TokenExpiryInMinutes { get; set; }
        public int RefreshTokenExpiryInMonths { get; set; }
    }
}

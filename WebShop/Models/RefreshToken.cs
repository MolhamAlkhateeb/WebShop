using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebShop.Data;

namespace WebShop.Models
{
    public class RefreshToken
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public string Jti { get; set; }
        public bool IsRevoked { get; set; }

        [JsonIgnore]
        public ApplicationUser User { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
    }
}

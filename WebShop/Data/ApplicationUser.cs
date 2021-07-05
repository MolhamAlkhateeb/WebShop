using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public RefreshToken RefreshToken { get; set; }

        public List<Order> Orders { get; set; }
        public List<Address> Addresses { get; set; }
        //public Address DefaultShippingAddress { get; set; }
    }
}

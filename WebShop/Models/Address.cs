using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data;

namespace WebShop.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Appartment { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}

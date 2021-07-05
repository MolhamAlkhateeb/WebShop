using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data;

namespace WebShop.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreateDate => DateTime.UtcNow;
        public DateTime DeliverDate { get; set; }
        public bool IsDelivered { get; set; } = false;
        public List<Product> Products { get; set; } = new List<Product>();
        public Address Address { get; set; }
        public bool PaymentConfirmed { get; set; } = false;

        //[Column(TypeName = "decimal(8, 2)")]
        //public decimal TotalPrice => Products.Sum(product => product.Price);

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        
    }
}

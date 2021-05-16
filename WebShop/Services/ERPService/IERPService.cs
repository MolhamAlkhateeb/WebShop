using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Services.ERPService
{
    public interface IERPService
    {
        Task<List<Product>> GetProducts();
        Task<List<Product>> GetProducts(SearchCriteria criteria);
    }
}

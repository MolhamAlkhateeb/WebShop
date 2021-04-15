using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProduct(int id);
        Task<IEnumerable<Product>> GetProducts(SearchCriteria criteria);
    }
}

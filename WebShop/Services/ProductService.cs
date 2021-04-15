using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebShop.Extensions;
using WebShop.Models;
using WebShop.Services.Interfaces;

namespace WebShop.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Product> GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProducts(SearchCriteria criteria)
        {
            var response = await _httpClient.GetAsync($"/api/products/{ criteria.ToQueryString()}");
            response.EnsureSuccessStatusCode();
            IEnumerable<Product> products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            return products;
        }
    }
}

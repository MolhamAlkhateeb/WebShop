using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;
using WebShop.Services.ERPService;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IERPService erpService;

        public ProductsController(ApplicationDbContext context, IProductService productService,IERPService erpService)
        {
            _context = context;
            _productService = productService;
            this.erpService = erpService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery]SearchCriteria criteria)
        {
            List<Product> products = await erpService.GetProducts(criteria);
            return Ok(products);
            //IEnumerable<Product> products = await _productService.GetProducts(criteria);
            //return products.ToList();
            //return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
    }
}

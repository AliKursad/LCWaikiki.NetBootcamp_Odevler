using ExampleWebAPI.DTOs;
using ExampleWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ExampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;
        string dateTimeNow = $"{DateTime.Now.ToShortDateString()} | {DateTime.Now.ToShortTimeString()}";
        public ProductController(ProductContext context)
        {
            _context = context;
            if (_context.Products.Count() == 0)
            {
                _context.Products.Add(new Product
                {
                    Name = "Kazak",
                    Price = 200,
                    InStock = 1500,
                    CreatedDate = dateTimeNow
                });
                _context.Products.Add(new Product
                {
                    Name = "Gömlek",
                    Price = 120,
                    InStock = 2000,
                    CreatedDate = dateTimeNow
                });
                _context.Products.Add(new Product
                {
                    Name = "Ayakkabı",
                    Price = 650,
                    InStock = 1200,
                    CreatedDate = dateTimeNow
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products.OrderBy(x => x.Id).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody]ProductAddDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Product product = new Product();
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.InStock = productDto.InStock;
            product.CreatedDate = dateTimeNow;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok($"{product.Name} adlı ürün başarıyla eklenmiştir.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok($"{id} nolu ürün başarıyla silinmiştir.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Product productToUpdate = await _context.Products.FindAsync(id);
            if (productToUpdate == null)
            {
                return NotFound();
            }
            if (product.Name != null) productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.InStock = product.InStock;
            productToUpdate.UpdatedDate = dateTimeNow;
            await _context.SaveChangesAsync();

            return Ok($"{id} nolu ürün başarıyla güncellenmiştir.");
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProduct([FromQuery] string contains, [FromQuery] decimal? maxPrice)
        {
            IQueryable<Product> products = _context.Products;

            if (!string.IsNullOrEmpty(contains))
            {
                products = products.Where(p => p.Name.Contains(contains,StringComparison.OrdinalIgnoreCase));
            }
            if (maxPrice != null)
            {
                products = products.Where(p => p.Price <= maxPrice);
            }

            return await products.ToListAsync();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Entities.Concrete;
using ProductAPI.Entities.Dtos;
using ProductAPI.Services.Abstract;
using ProductAPI.Shared.Utilities.Results.ComplexTypes;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var result = await _productService.GetAll();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var result = await _productService.Get(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string keyword)
        {
            var result = await _productService.Search(keyword);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductAddDto productAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.Add(productAddDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return Ok(result.Message);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.Delete(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.Update(productUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return Ok(result.Message);
                }
            }
            return BadRequest();
        }
    }
}

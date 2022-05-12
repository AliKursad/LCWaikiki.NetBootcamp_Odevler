using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Entities.Concrete;
using ProductAPI.Entities.Dtos;
using ProductAPI.Services.Abstract;
using ProductAPI.Shared.Utilities.Results.ComplexTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var result = await _categoryService.GetAll();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var result = await _categoryService.Get(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<IEnumerable<Category>>> SearchCategories(string keyword)
        {
            var result = await _categoryService.Search(keyword);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Add(categoryAddDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return Ok(result.Message);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.Delete(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Update(categoryUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return Ok(result.Message);
                }
            }
            return BadRequest();
        }
    }
}

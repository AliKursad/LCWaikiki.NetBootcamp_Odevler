using AutoMapper;
using ProductAPI.Data.Abstract;
using ProductAPI.Entities.Concrete;
using ProductAPI.Entities.Dtos;
using ProductAPI.Services.Abstract;
using ProductAPI.Shared.Utilities.Results.Abstract;
using ProductAPI.Shared.Utilities.Results.ComplexTypes;
using ProductAPI.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult> Add(CategoryAddDto categoryAddDto)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir.");
        }

        public async Task<IResult> Delete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId,c => c.Products);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error,"Böyle bir kategori bulunamadı.",null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null,c => c.Products);
            if (categories.Count>-1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error,"Hiçbir Kategori Bulunamadı.", null);
        }

        public async Task<IDataResult<CategoryListDto>> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                var categories = await _unitOfWork.Categories.GetAllAsync();
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories
                });
            }
            var searchedCategories = await _unitOfWork.Categories.SearchAsync(new List<Expression<Func<Category, bool>>>
            {
                (c) => c.Name.Contains(keyword),
                (c) => c.Description.Contains(keyword)
            });
            if (searchedCategories.Any())
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = searchedCategories
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error,"Bu kriterlere uyan herhangi bir kategori bulunamadı.",null);
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            var oldCategory = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            if (categoryUpdateDto.Description == null) categoryUpdateDto.Description = oldCategory.Description;
            var category = _mapper.Map(categoryUpdateDto,oldCategory);
            await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellenmiştir.");
        }
    }
}

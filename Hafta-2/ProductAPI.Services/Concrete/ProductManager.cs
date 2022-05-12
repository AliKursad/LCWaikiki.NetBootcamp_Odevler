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
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult> Add(ProductAddDto productAddDto)
        {
            var product = _mapper.Map<Product>(productAddDto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success,$"{productAddDto.Name} adlı ürün başarıyla eklenmiştir.");
        }

        public async Task<IResult> Delete(int productId)
        {
            var result = await _unitOfWork.Products.AnyAsync(p => p.Id == productId);
            if (result)
            {
                var product = await _unitOfWork.Products.GetAsync(p => p.Id == productId);
                await _unitOfWork.Products.DeleteAsync(product);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{product.Name} adlı ürün başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir ürün bulunamadı");
        }

        public async Task<IDataResult<ProductDto>> Get(int productId)
        {
            var product = await _unitOfWork.Products.GetAsync(p => p.Id == productId, p => p.Category);
            if (product != null)
            {
                return new DataResult<ProductDto>(ResultStatus.Success, new ProductDto
                {
                    Product = product,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProductDto>(ResultStatus.Error, "Böyle bir ürün bulunamadı.", null);
        }

        public async Task<IDataResult<ProductListDto>> GetAll()
        {
            var products = await _unitOfWork.Products.GetAllAsync(null, p => p.Category);
            foreach (var item in products)
            {
                item.Category.Products = null;
            }
            if (products.Count>-1)
            {
                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = products,
                    ResultStatus= ResultStatus.Success
                });
            }
            return new DataResult<ProductListDto>(ResultStatus.Error, "Ürünler bulunamadı.", null);
        }

        public async Task<IDataResult<ProductListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var products = await _unitOfWork.Products.GetAllAsync(p => p.CategoryId == categoryId, p => p.Category);
                if (products.Count > -1)
                {
                    return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                    {
                        Products = products,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ProductListDto>(ResultStatus.Error, "Ürünler bulunamadı.", null);
            }
            return new DataResult<ProductListDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı bulunamadı.", null);
        }

        public async Task<IDataResult<ProductListDto>> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                var products = await _unitOfWork.Products.GetAllAsync(null, p => p.Category);
                foreach (var item in products)
                {
                    item.Category.Products = null;
                }
                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = products
                });
            }
            var searchedProducts = await _unitOfWork.Products.SearchAsync(new List<Expression<Func<Product, bool>>>
            {
                (p) => p.Name.Contains(keyword),
                (p) => p.Description.Contains(keyword)
            });
            if (searchedProducts.Any())
            {
                foreach (var item in searchedProducts)
                {
                    item.Category = await _unitOfWork.Categories.GetAsync(c => c.Id == item.CategoryId);
                    item.Category.Products = null;
                }
                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = searchedProducts
                });
            }
            return new DataResult<ProductListDto>(ResultStatus.Error, "Bu kriterlere uyan herhangi bir ürün bulunamadı.",null);
        }

        public async Task<IResult> Update(ProductUpdateDto productUpdateDto)
        {
            var oldProduct = await _unitOfWork.Products.GetAsync(c => c.Id == productUpdateDto.Id);
            if (productUpdateDto.Description == null) productUpdateDto.Description = oldProduct.Description;
            var product = _mapper.Map(productUpdateDto, oldProduct);
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{productUpdateDto.Name} adlı ürün başarıyla güncellenmiştir.");
        }
    }
}

using ProductAPI.Entities.Concrete;
using ProductAPI.Entities.Dtos;
using ProductAPI.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Services.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<ProductDto>> Get(int productId);
        Task<IDataResult<ProductListDto>> GetAll();
        Task<IDataResult<ProductListDto>> GetAllByCategory(int categoryId);
        Task<IResult> Add(ProductAddDto productAddDto);
        Task<IResult> Update(ProductUpdateDto productUpdateDto);
        Task<IResult> Delete(int productId);
        Task<IDataResult<ProductListDto>> Search(string keyword);
    }
}

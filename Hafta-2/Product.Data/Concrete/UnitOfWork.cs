using ProductAPI.Data.Abstract;
using ProductAPI.Data.Concrete.EntityFramework.Contexts;
using ProductAPI.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductAPIContext _context;
        private EfProductRepository _productRepository;
        private EfCategoryRepository _categoryRepository;
        public UnitOfWork(ProductAPIContext context)
        {
            _context = context;
        }
        public IProductRepository Products => _productRepository ?? new EfProductRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ?? new EfCategoryRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

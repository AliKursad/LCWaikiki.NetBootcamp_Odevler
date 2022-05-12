using Microsoft.Extensions.DependencyInjection;
using ProductAPI.Data.Abstract;
using ProductAPI.Data.Concrete;
using ProductAPI.Data.Concrete.EntityFramework.Contexts;
using ProductAPI.Services.Abstract;
using ProductAPI.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ProductAPIContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IProductService, ProductManager>();
            return serviceCollection;
        }
    }
}

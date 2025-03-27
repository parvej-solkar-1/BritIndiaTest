using Products.BusinessLayer;
using Products.BusinessLayer.Interfaces;
using Products.DataAccess;
using Products.DataAccess.Repositories;

namespace ProductServices.Extensions
{
    public static class DependancyExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IProductsUnitOfWork, ProductsUnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            return services;
        }
    }
}

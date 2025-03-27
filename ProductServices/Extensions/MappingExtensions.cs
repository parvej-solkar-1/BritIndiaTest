using ProductServices.Mapping;

namespace ProductServices.Extensions
{
    public static class MappingExtensions
    {
        public static IServiceCollection RegisterMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductMapper).Assembly);
            return services;
        }
    }
}

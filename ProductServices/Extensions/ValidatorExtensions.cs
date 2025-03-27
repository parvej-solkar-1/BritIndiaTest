using FluentValidation;
using FluentValidation.AspNetCore;

namespace ProductServices.Extensions
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection RegisterValidations<T>(this IServiceCollection services)
        {
            // services.AddHttpContextAccessor() is required to check currunt HTTP method in fluent validations
            // Please make sure it is added in Program.cs

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<T>();
            return services;
        }
    }
}

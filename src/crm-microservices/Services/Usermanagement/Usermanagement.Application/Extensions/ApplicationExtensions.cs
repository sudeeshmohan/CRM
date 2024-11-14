using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Usermanagement.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection ApplicationServiceCollections(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

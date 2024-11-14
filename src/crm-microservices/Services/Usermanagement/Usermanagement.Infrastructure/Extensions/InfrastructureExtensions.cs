using Microsoft.Extensions.DependencyInjection;
using Usermanagement.Domain.Interfaces;
using Usermanagement.Infrastructure.Persistence.UserDetails;

namespace Usermanagement.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection InsfrastructureServiceCollections(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}

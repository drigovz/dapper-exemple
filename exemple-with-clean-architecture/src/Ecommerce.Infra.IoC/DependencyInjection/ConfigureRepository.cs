using Ecommerce.Domain.Interfaces;
using Ecommerce.Infra.Data.Helpers;
using Ecommerce.Infra.Data.Repository.Customers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.IoC.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            ConnectionStringConfig.SetConfig(configuration);
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Contracts;
using API_PJ01_Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace API_PJ01_Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(name: "DefaultConnection"));
            });

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString(name: "RedisConnection"))
            );

            return services;
        }
    }
}

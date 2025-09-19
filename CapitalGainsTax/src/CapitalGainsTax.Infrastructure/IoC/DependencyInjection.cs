using CapitalGainsTax.Application.Interfaces;
using CapitalGainsTax.Infrastructure.Data.Context;
using CapitalGainsTax.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CapitalGainsTax.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // configura conexão com banco de dados (SQL Server)
            services.AddDbContext<CapitalGainsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // registra repositórios
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();

            return services;
        }
    }
}
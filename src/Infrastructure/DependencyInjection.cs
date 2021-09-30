using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PLS.Application.Common.Interfaces;
using PLS.Infrastructure.Configurations;
using PLS.Infrastructure.Persistence;

namespace PLS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(typeof(ConfigFactory).Assembly.Location))
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true);

            services.AddSingleton<IConfiguration>(configBuilder.Build());
            IConfiguration configuration = services.BuildServiceProvider().CreateScope().ServiceProvider.GetService<IConfiguration>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IConfigFactory, ConfigFactory>();

            return services;
        }
    }
}

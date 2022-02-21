using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PLS.Application.Common.Interfaces;
using PLS.Application.Common.Logging;
using PLS.Infrastructure.Configurations;
using PLS.Infrastructure.Logging;
using PLS.Infrastructure.Persistence;
using Serilog;

namespace PLS.Infrastructure
{
    public class DependencyInjection : Autofac.Module
    {
        private readonly Assembly _callingAssembly;
        public DependencyInjection(Assembly assembly)
        {
            _callingAssembly = assembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(typeof(ConfigFactory).Assembly.Location))
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true);


            IConfiguration configuration = configBuilder.Build();
            builder.RegisterInstance(configuration).As<IConfiguration>();

            // The Microsoft.Extensions.DependencyInjection.ServiceCollection
            // has extension methods provided by other .NET Core libraries to
            // register services with DI.
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IConfigFactory, ConfigFactory>();

            builder.Register<ILogger>((c, p) =>
            {
                return new LoggerConfiguration()
                  .ReadFrom.Configuration(configuration)
                  .Enrich.WithProperty("AssemblyVersions", new Dictionary<string, string>
                  {
                      {
                          _callingAssembly.GetName().Name,
                          _callingAssembly.GetName().Version.ToString()
                      },
                      {
                          typeof(Application.DependencyInjection).Assembly.GetName().Name,
                          typeof(Application.DependencyInjection).Assembly.GetName().Version.ToString()
                      },
                      {
                          ThisAssembly.GetName().Name,
                          ThisAssembly.GetName().Version.ToString()
                      },
                  })
                  .CreateLogger();
            });
            services.AddScoped<ILogTrace, LogTrace>();
            builder.Populate(services);
        }
    }
}

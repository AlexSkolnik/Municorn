using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Municorn.DAL.Contexts;
using Municorn.Infrastructure.Commands;
using Municorn.Infrastructure.Interfaces;
using Municorn.Infrastructure.Repositories;
using Municorn.Infrastructure.Requests;
using Municorn.Infrastructure.Services;
using Municorn.WebAPI.Configurations;
using Municorn.WebAPI.Infrastructures.Filters;
using Municorn.WebAPI.Infrastructures.Options;

using Serilog;
using Serilog.Extensions.Logging;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Municorn.WebAPI.Infrastructures.Extensions
{
    public static class ConfigureServicesExtentions
    {
        public static IServiceCollection ConfigureLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var providers = new LoggerProviderCollection();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            services.AddSingleton(providers);
            services.AddSingleton<ILoggerFactory>(sc =>
            {
                var providerCollection = sc.GetService<LoggerProviderCollection>();
                var factory = new SerilogLoggerFactory(null, true, providerCollection);

                foreach (var provider in sc.GetServices<ILoggerProvider>())
                {
                    factory.AddProvider(provider);
                }

                return factory;
            });

            return services;
        }

        public static IServiceCollection ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning();
            services.AddVersionedApiExplorer(opt => opt.GroupNameFormat = "'v'VVV");
            return services;
        }

        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            var assembly = typeof(CreateAndroidNotificationCommand).GetTypeInfo().Assembly;
            services.AddMediatR(assembly);
            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddApiVersioning();
            services.AddVersionedApiExplorer(opt => opt.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                var assembly = Assembly.GetExecutingAssembly();
                options.CustomSchemaIds(x => x.FullName);
                var xmlFile = $"{assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IServiceCollection ConfigureCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<INotificationManager<CreateiOSNotificationRequest>, IOSNotificationManager>();
            services.AddSingleton<INotificationManager<CreateAndroidNotificationRequest>, AndroidNotificationManager>();

            return services;
        }

        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<GlobalExceptionFilter>();
                    options.Filters.Add<JsonStringEnumFilter>();
                })
                .AddJsonOptions(opts =>
                {
                    var enumConverter = new JsonStringEnumConverter();
                    opts.JsonSerializerOptions.Converters.Add(enumConverter);
                });

            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConfig = new DatabaseConnectionOptions();
            configuration.GetSection(nameof(DatabaseConnectionOptions)).Bind(dbConfig);

            services.AddDbContext<MunicornDbContext>(builder =>
            {
                builder.UseNpgsql(dbConfig.ConnectionString, b => b.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null));
            });

            services.AddScoped<IMunicornDbContext, MunicornDbContext>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            return services;
        }
    }
}

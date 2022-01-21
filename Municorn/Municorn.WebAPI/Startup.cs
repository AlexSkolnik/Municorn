using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Municorn.WebAPI.Infrastructures.Extensions;

using Serilog;

namespace Municorn.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public bool SwaggerOn => Configuration?.GetValue(nameof(SwaggerOn), false) ?? false;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (SwaggerOn)
            {
                services.ConfigureSwagger();
            }

            services.ConfigureVersioning();
            services.ConfigureMediatR();
            services.ConfigureLogger(Configuration);
            services.ConfigureDatabase(Configuration);
            services.ConfigureCustomServices();
            services.ConfigureMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (SwaggerOn)
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

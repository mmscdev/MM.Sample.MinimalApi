using Carter;
using Microsoft.OpenApi.Models;
using System.Data.SqlClient;

namespace MM.MinimalApi.Package.Extensions
{
    public static class SwaggerExtensions
    {
        public static WebApplicationBuilder AddOpenApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwagger();

            return builder;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Description = "Packages",
                    Title = "Packages",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Developers",
                        Email = "teste@minimalapi.com"
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app, string routeprefix)
        {
            app.UseSwagger();
            app.UseSwaggerUI(_ =>
            {
                _.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                _.RoutePrefix = routeprefix;
            }
            );

            return app;
        }
    }
}

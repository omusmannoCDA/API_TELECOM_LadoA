using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace API_TELECOM_LadoA.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Pruebas con Oracle",
                    Version = "v3",
                    Description = "Pruebas de conexion a base de datos Oracle",
                    Contact = new OpenApiContact
                    {
                        Name = "CDA",
                        Url = new Uri("http://www.cdainfo.com/es/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                    },
                });
            });
            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba Oracle");
                options.DocumentTitle = "Prueba Oracle";
            });
            return app;
        }
    }
}

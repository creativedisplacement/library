using Microsoft.AspNetCore.Builder;

namespace Library.WebApi.Configuration
{
    public static class SwaggerConfigExtension
    {
        public static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });
        }
    }
}
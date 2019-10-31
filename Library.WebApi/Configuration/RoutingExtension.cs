using Microsoft.AspNetCore.Builder;

namespace Library.WebApi.Configuration
{
    public static class RoutingExtension
    {
        public static void UseRoutingConfig(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
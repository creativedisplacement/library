using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.WebApi.Configuration
{
    public static class DbContextExtension
    {
        public static void AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("LibraryDB")));
        }
    }
}
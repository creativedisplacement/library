using FluentValidation.AspNetCore;
using Library.Application.Book.Commands.CreateBook;
using Microsoft.Extensions.DependencyInjection;

namespace Library.WebApi.Configuration
{
    public static class FluentValidationExtension
    {
        public static void AddFluentValidationConfig(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateBookCommandValidator>());
        }
    }
}
using Library.Application.Categories.Queries.GetCategories;
using Library.Application.Infrastructure;
using Library.Application.Interfaces;
using Library.Infrastructure;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Library.WebApi.Configuration
{
    public static class DIConfigurationExtension
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddTransient<IMailingService, MailingService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(GetCategoriesQueryHandler).GetTypeInfo().Assembly);
        }
    }
}
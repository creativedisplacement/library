using FluentValidation.AspNetCore;
using Library.Application.Book.Commands.CreateBook;
using Library.Application.Categories.Queries.GetCategories;
using Library.Application.Infrastructure;
using Library.Application.Interfaces;
using Library.Infrastructure;
using Library.Persistence;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Library.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateBookCommandValidator>());

            services.AddTransient<IMailingService, MailingService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(GetCategoriesQueryHandler).GetTypeInfo().Assembly);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("LibraryDB")));

            services.AddSwaggerDocument();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.AddFile("Logs/library-{Date}.txt");
        }
    }
}
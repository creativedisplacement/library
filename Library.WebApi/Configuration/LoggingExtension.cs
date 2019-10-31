using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Library.WebApi.Configuration
{
    public static class LoggingExtension
    {
        public static void UseLoggingConfig(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/library-{Date}.txt");
        }
    }
}
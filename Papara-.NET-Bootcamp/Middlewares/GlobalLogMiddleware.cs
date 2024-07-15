using System.Diagnostics;

namespace Papara_Bootcamp.Middlewares
{
    public class ActionEntryLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ActionEntryLoggingMiddleware> _logger;

        public ActionEntryLoggingMiddleware(RequestDelegate next, ILogger<ActionEntryLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Entering action: {context.Request.Path}");

            await _next(context);
        }
    }

}

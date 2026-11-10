namespace Library_Management_API.GlobalExceptionMiddleware
{
    public class GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An unexpected error occurred. Path: {Path}",
                    context.Request.Path);

                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    Message = "An unexpected error occurred.",
#if DEBUG
                    Detail = ex.Message
#endif
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}

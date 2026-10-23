namespace Library_Management_API.GlobalExceptionMiddleware
{
    public static class ExceptionMiddleware
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(
        this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}

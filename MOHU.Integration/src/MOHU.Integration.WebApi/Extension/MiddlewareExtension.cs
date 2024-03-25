using MOHU.Integration.WebApi.Middleware;

namespace MOHU.Integration.WebApi.Extension
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseLanguageMiddleware(
      this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LanguageHeaderMiddleware>();
        }
        public static IApplicationBuilder UseGlobalExceptionHandler(
 this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}

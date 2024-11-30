using MOHU.Integration.WebApi.Common.Middleware;

namespace MOHU.Integration.WebApi.Common.Extension
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseLanguageMiddleware(
      this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomHeadersMiddleware>();
        }
        public static IApplicationBuilder UseGlobalExceptionHandler(
 this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}

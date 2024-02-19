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
    }
}

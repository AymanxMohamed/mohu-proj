using MOHU.ExternalIntegration.Shared;
using System.Globalization;

namespace MOHU.Externalintegration.WebApi.Middleware
{
    public class LanguageHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public LanguageHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string languageHeaderValue = context?.Request?.Headers[Header.Language];
            var culture = Globals.DefaultLanguageHeaderCulture;
            // Set the current culture based on the language header value
            if (!string.IsNullOrEmpty(languageHeaderValue))
            {
                if (languageHeaderValue.Contains("ar"))
                    culture = Globals.ArabicLanguageHeaderCulture;
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
            }

            await _next(context);
        }
    }




}

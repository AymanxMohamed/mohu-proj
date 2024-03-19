using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Shared;
using System.Globalization;

namespace MOHU.Integration.WebApi.Middleware
{
    public class LanguageHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRequestInfo _requestInfo;
        public LanguageHeaderMiddleware(RequestDelegate next, IRequestInfo requestInfo)
        {
            _next = next;
            _requestInfo = requestInfo;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string languageHeaderValue = context?.Request?.Headers[Header.Language];
            var culture = Globals.DefaultLanguageHeaderCulture;
            // Set the current culture based on the language header value
            if (!string.IsNullOrEmpty(languageHeaderValue))
            {
                if(languageHeaderValue.Contains("ar"))
                    culture = Globals.ArabicLanguageHeaderCulture;
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
            }

            string origin = context?.Request?.Headers[Header.Origin];
            if (!string.IsNullOrEmpty(origin))
                _requestInfo.Origin = Convert.ToInt32(origin);
     
            await _next(context);
        }
    }
}

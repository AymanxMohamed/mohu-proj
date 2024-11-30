using System.Globalization;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Shared;

namespace MOHU.Integration.WebApi.Common.Middleware
{
    public class CustomHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRequestInfo _requestInfo;
        public CustomHeadersMiddleware(RequestDelegate next, IRequestInfo requestInfo)
        {
            _next = next;
            _requestInfo = requestInfo;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string languageHeaderValue = context?.Request?.Headers[Header.Language];
            var culture = Globals.DefaultLanguageHeaderCulture;
            _requestInfo.Language = "en";
            // Set the current culture based on the language header value
            if (!string.IsNullOrEmpty(languageHeaderValue))
            {
                if (languageHeaderValue.Contains("ar"))
                {
                    culture = Globals.ArabicLanguageHeaderCulture;
                    _requestInfo.Language = "ar";
                }
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

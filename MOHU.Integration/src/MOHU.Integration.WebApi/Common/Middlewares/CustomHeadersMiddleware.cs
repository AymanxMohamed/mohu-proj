using System.Globalization;
using MOHU.Integration.Shared;

namespace MOHU.Integration.WebApi.Common.Middlewares;

public class CustomHeadersMiddleware(RequestDelegate next, IRequestInfo requestInfo)
{
    public async Task InvokeAsync(HttpContext context)
    {
        string languageHeaderValue = context?.Request?.Headers[Header.Language];
        var culture = Globals.DefaultLanguageHeaderCulture;
        requestInfo.Language = "en";
        // Set the current culture based on the language header value
        if (!string.IsNullOrEmpty(languageHeaderValue))
        {
            if (languageHeaderValue.Contains("ar"))
            {
                culture = Globals.ArabicLanguageHeaderCulture;
                requestInfo.Language = "ar";
            }
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
        }

        string origin = context?.Request?.Headers[Header.Origin];
        if (!string.IsNullOrEmpty(origin))
            requestInfo.Origin = Convert.ToInt32(origin);
     
        await next(context);
    }
}
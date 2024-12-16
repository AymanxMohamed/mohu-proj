using System.Globalization;
using MOHU.Integration.Shared;

namespace MOHU.Integration.WebApi.Common.Middlewares;

public class CustomHeadersMiddleware(RequestDelegate next, IRequestInfo requestInfo)
{
    public async Task InvokeAsync(HttpContext? context)
    {
        if (context == null)
        {
            return;
        }
        
        var languageHeaderValue = context.Request.Headers[Header.Language].FirstOrDefault();
        var culture = Globals.DefaultLanguageHeaderCulture;
        
        requestInfo.Language = "en";
        
        if (!string.IsNullOrEmpty(languageHeaderValue))
        {
            if (languageHeaderValue.Contains("ar", StringComparison.OrdinalIgnoreCase))
            {
                culture = Globals.ArabicLanguageHeaderCulture;
                requestInfo.Language = "ar";
            }
            
            try
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
            }
            catch (CultureNotFoundException)
            {
                CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
            }
        }

        var originHeaderValue = context.Request.Headers[Header.Origin].FirstOrDefault();
        
        if (!string.IsNullOrEmpty(originHeaderValue) && int.TryParse(originHeaderValue, out var origin))
        {
            requestInfo.Origin = origin;
        }
        else
        {
            requestInfo.Origin = default;
        }

        await next(context);
    }
}
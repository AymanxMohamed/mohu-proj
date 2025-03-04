using System.Text;
using Common.Crm.Infrastructure.Common.Constants;

namespace Common.Crm.Infrastructure.Common.Extensions;

public static class HttpRequestMessageExtensions
{
    public static HttpRequestMessage AddRequestBody(this HttpRequestMessage request, object? body = null)
    {
        if (body == null) return request;
            
        var jsonBody = AppSerializer.Instance.Serialize(body);
        request.Content = new StringContent(jsonBody, Encoding.UTF8, mediaType: HttpHeaders.ApplicationJson);
        return request;
    }
}
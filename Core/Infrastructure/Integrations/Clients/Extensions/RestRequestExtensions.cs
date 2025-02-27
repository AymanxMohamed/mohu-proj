using Core.Domain.Integrations.Clients;
using Core.Infrastructure.Integrations.Clients.Constants;

namespace Core.Infrastructure.Integrations.Clients.Extensions;

public static class RestRequestExtensions
{
    public static RestRequest AddRequestBody(this RestRequest request, object body)
    {
        request.AddParameter(
            name: HttpHeaderConstants.MimeTypes.ApplicationJson, 
            value: JsonConvert.SerializeObject(body), 
            ParameterType.RequestBody);

        return request;
    }   
    
    public static RestRequest AddResourceParameters(
        this RestRequest request, 
        IEnumerable<ResourceParameter> resourceParameters)
    {
        foreach (var resourceParameter in resourceParameters
                     .Where(resourceParameter => resourceParameter.Value is not null))
        {
            request.AddResourceParameter(resourceParameter);
        }

        return request;
    }

    public static RestRequest AddResourceParameter(this RestRequest request, ResourceParameter resourceParameter) 
    {
        if (resourceParameter.Value is null)
        {
            return request;
        }
        
        request.AddParameter(
            resourceParameter.Name, 
            JsonConvert.SerializeObject(resourceParameter.Value), 
            resourceParameter.ParameterType,
            encode: false);

        return request;
    }

    public static RestRequest AddRequestParameters(
        this RestRequest request, 
        Dictionary<string, object?> parameters)
    {
        foreach (var parameter in parameters)
        {
            if (parameter.Value is not null)
            {
                request.AddParameter(parameter.Key, parameter.Value, ParameterType.QueryString);
            }
        }

        return request;
    }
}
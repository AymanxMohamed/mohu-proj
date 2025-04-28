using Core.Application.Integrations.Clients;
using Core.Domain.ErrorHandling.Extensions;
using Core.Domain.ErrorHandling.Models;
using Core.Domain.Integrations.Clients;
using Core.Infrastructure.Integrations.Clients.Constants;
using Core.Infrastructure.Integrations.Clients.Extensions;
using Core.Infrastructure.Integrations.Clients.Settings;

namespace Core.Infrastructure.Integrations.Clients;

public abstract class RestClientService : IRestClientService
{
    private readonly RestClient _restClient;
    private readonly ILogger<RestClientService> _logger;

    protected RestClientService(RestClientSettings clientSettings, ILogger<RestClientService> logger)
    {
        _logger = logger;
        _restClient = new RestClient(clientSettings.BaseUrl);
            
        _restClient.AddDefaultHeader(
            HttpHeaderConstants.Headers.Accept, 
            HttpHeaderConstants.MimeTypes.ApplicationJson);
        
        _restClient.AddDefaultHeader(
            HttpHeaderConstants.Headers.ContentType, 
            HttpHeaderConstants.MimeTypes.ApplicationJson);

        if (clientSettings.AuthorizationHeaderKey is not null && clientSettings.AuthorizationHeaderValue is not null)
        {
            _restClient.AddDefaultHeader(
                clientSettings.AuthorizationHeaderKey, 
                clientSettings.AuthorizationHeaderValue);
        }
    }

    public void SetAuthorizationToken(string authorizationToken)
    {
        _restClient.DefaultParameters.RemoveParameter(
            name: HttpHeaderConstants.Headers.Authorization, 
            ParameterType.HttpHeader);
        
        _restClient.AddDefaultHeader(
            HttpHeaderConstants.Headers.Authorization, 
            authorizationToken);
    }

    public virtual ErrorOr<TEntity?> PrepareAndExecuteRequest<TEntity>(
        string resourceUrl, 
        Method method = Method.Get, 
        object? body = null,
        List<ResourceParameter>? resourceParameters = null)
    {
        var request = PrepareRequest(resourceUrl, method: method, body: body, resourceParameters: resourceParameters);
        
        return ExecuteRequest<TEntity>(request);
    }

    public virtual RestRequest PrepareRequest(
        string resourceUrl, 
        Method method = Method.Get, 
        object? body = null,
        List<ResourceParameter>? resourceParameters = null)
    {
        var request = new RestRequest(resourceUrl, method);
       
        if (body is not null)
        {
            request.AddRequestBody(body);
        }

        if (resourceParameters is not null)
        {
            request.AddResourceParameters(resourceParameters);
        }
        
        return request;
    }
    
    public virtual ErrorOr<TEntity?> ExecuteRequest<TEntity>(RestRequest request)
    {
        _logger.LogInformation(
            "Executing request for {RequestMethod} {RequestResource} {@RequestParameters}", 
            request.Method, 
            request.Resource, 
            request.Parameters);
        
        var response = _restClient.Execute(request);
        
        if (response.IsSuccessful)
        {
            _logger.LogInformation(
                "Request to {RequestResource} succeeded with status code {StatusCode} and content {@Content}", 
                request.Resource, 
                response.StatusCode,
                response.Content);
            
            return string.IsNullOrWhiteSpace(response.Content)
                ? default
                : JsonConvert.DeserializeObject<TEntity>(response.Content);
        }
        
        _logger.LogWarning(
            "Request to {RequestResource} failed with status code {StatusCode}, Error: {ErrorMessage} and content {@Content}", 
            request.Resource,
            response.StatusCode,
            response.ErrorMessage,
            response.Content);

        if (string.IsNullOrWhiteSpace(response.Content))
        {
            return response.StatusCode.ToErrorOrError(response.ErrorMessage);
        }

        try
        {
            var problemDetails = JsonConvert.DeserializeObject<CoreProblemDetails>(response.Content);

            if (problemDetails?.ModelState is null)
            {
                return response.StatusCode.ToErrorOrError(response.ErrorMessage);
            }
        }
        catch
        {
            // ignored
        }

        return response.StatusCode.ToErrorOrError(response.Content);
    }
}
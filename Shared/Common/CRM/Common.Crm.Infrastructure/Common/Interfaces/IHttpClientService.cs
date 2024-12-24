namespace Common.Crm.Infrastructure.Common.Interfaces;

public interface IHttpClientService
{
    HttpClient GetHttpClient();
    Task<T> ExecuteRequest<T>(HttpRequestMessage httpRequestMessage);

    HttpRequestMessage PrepareHttpRequest(
        HttpMethod method,
        string resourceUrl,
        object? body = null);

    Task<T> PrepareAndExecuteRequest<T>(HttpMethod method,
        string resourceUrl,
        object? body = null);
}
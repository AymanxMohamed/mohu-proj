using Core.Domain.Integrations.Clients;

namespace Core.Application.Integrations.Clients;

public interface IRestClientService
{
    public ErrorOr<TEntity?> PrepareAndExecuteRequest<TEntity>(
        string resourceUrl,
        Method method = Method.Get,
        object? body = null,
        List<ResourceParameter>? resourceParameters = null);
    
    public ErrorOr<TEntity?> ExecuteRequest<TEntity>(RestRequest request);
    
    public RestRequest PrepareRequest(
        string resourceUrl,
        Method method = Method.Get,
        object? body = null,
        List<ResourceParameter>? resourceParameters = null);
}
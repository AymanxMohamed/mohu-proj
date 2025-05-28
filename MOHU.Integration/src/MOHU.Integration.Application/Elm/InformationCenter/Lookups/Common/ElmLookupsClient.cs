using Core.Domain.ErrorHandling.Extensions;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using RestSharp;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;

public class ElmLookupsClient(
    IElmInformationCenterClient client,
    ElmInformationCenterApiSettings settings) : IElmLookupsClient
{
    public ErrorOr<TLookupData> GetLookups<TLookupData>(
        string lookupCollectionName,
        ElmFilterRequest? request = null)
    {
        request ??= ElmFilterRequest.Create();

        request.AddDefaultPaginationIfNull();
        
        return client
            .PrepareAndExecuteRequest<ElmInformationCenterResponseRoot<TLookupData>>(
                resourceUrl: $"{settings.LookupsMainCollection}/{lookupCollectionName}",
                method: Method.Post,
                body: request ?? new object())
            .Then(x => x.EnsureNotNull())
            .Then(x => x.EnsureSuccessResult());
    }
}
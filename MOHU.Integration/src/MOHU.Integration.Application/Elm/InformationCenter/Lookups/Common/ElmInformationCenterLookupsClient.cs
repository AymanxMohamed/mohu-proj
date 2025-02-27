using Core.Domain.ErrorHandling.Extensions;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using RestSharp;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;

public abstract class ElmInformationCenterLookupsClient(
    string lookupCollectionName,
    IElmInformationCenterClient client)
{
    protected ErrorOr<TLookupData> GetLookups<TLookupData>(
        FilterRequest? request = null)
    {
        return client
            .PrepareAndExecuteRequest<ElmInformationCenterResponseRoot<TLookupData>>(
                resourceUrl: $"{lookupCollectionName}",
                method: Method.Post,
                body: request)
            .Then(x => x.EnsureNotNull())
            .Then(x => x.EnsureSuccessResult());
    }
}
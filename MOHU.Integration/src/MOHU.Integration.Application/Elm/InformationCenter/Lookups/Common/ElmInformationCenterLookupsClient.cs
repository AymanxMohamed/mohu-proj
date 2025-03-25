using Core.Domain.ErrorHandling.Extensions;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Domain.Features.Common.CrmEntities;
using RestSharp;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;

public abstract class ElmInformationCenterLookupsClient(
    string lookupCollectionName,
    IElmInformationCenterClient client,
    ElmInformationCenterApiSettings settings)
{
    protected ErrorOr<TLookupData> GetLookups<TLookupData>(
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
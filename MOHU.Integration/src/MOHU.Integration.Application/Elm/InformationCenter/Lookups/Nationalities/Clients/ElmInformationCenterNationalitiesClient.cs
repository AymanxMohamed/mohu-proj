using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Nationalities.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Nationalities.Clients;

internal class ElmInformationCenterNationalitiesClient(IElmInformationCenterClient client)
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: $"{LookupsConstants.MainCollectionName}/nationalities",
            client),
        IElmInformationCenterNationalitiesClient
{
    public ErrorOr<List<ElmNationalityResponse>> GetAll(ElmFilterRequest? request = null) =>
        GetLookups<List<ElmNationalityResponse>>(request);
}
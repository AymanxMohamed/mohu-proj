using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Nationalities.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Nationalities.Clients;

internal class ElmInformationCenterNationalitiesClient(
    IElmInformationCenterClient client,
    ElmInformationCenterApiSettings settings)
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: "nationalities",
            client,
            settings),
        IElmInformationCenterNationalitiesClient
{
    public ErrorOr<List<ElmNationalityResponse>> GetAll(ElmFilterRequest? request = null) =>
        GetLookups<List<ElmNationalityResponse>>(request);
}
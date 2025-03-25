using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Houses.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Houses.Clients;

internal class ElmInformationCenterHousesClient(
    IElmInformationCenterClient client,
    ElmInformationCenterApiSettings settings)
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: "houses",
            client,
            settings),
        IElmInformationCenterHousesClient
{
    public ErrorOr<List<ElmHouseResponse>> GetAll(ElmFilterRequest? request = null)
    {
        request ??= ElmFilterRequest.Create();

        request.AddSortColumn(ElmSortItem.CreateDesc(nameof(ElmCompanyResponse.TimeStamp).ToLower()));

        return GetLookups<List<ElmHouseResponse>>(request);
    }
}
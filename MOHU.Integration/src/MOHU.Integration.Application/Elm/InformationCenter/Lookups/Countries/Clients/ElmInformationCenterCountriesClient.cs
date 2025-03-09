using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Clients;

internal class ElmInformationCenterCountriesClient(IElmInformationCenterClient client)
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: $"{LookupsConstants.MainCollectionName}/countries",
            client),
        IElmInformationCenterCountriesClient
{
    public ErrorOr<List<ElmCountryResponse>> GetAll(ElmFilterRequest? request = null) => 
        GetLookups<List<ElmCountryResponse>>(request);
}
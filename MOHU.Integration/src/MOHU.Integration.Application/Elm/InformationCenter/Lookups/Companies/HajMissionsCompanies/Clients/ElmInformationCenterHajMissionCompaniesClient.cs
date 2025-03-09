using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.HajMissionsCompanies.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.HajMissionsCompanies.Clients;

internal class ElmInformationCenterHajMissionCompaniesClient(IElmInformationCenterClient client)
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: $"{LookupsConstants.MainCollectionName}/haj-missions",
            client),
        IElmInformationCenterHajMissionCompaniesClient
{
    public ErrorOr<List<ElmHajMissionCompanyResponse>> GetAll(ElmFilterRequest? request = null)
    {
        request ??= ElmFilterRequest.Create();

        request.AddSortColumn(ElmSortItem.CreateDesc(nameof(ElmCompanyResponse.TimeStamp).ToLower()));

        return GetLookups<List<ElmHajMissionCompanyResponse>>(request);
    }
 }
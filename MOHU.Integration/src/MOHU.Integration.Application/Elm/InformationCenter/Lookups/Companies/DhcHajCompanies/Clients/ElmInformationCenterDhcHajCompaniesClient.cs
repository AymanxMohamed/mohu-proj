using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Clients;

internal class ElmInformationCenterDhcHajCompaniesClient(
    IElmInformationCenterClient client, 
    ElmInformationCenterApiSettings settings) 
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: "dhc-haj-companies",
            client,
            settings),
        IElmInformationCenterDhcHajCompaniesClient
{
    public ErrorOr<List<ElmDhcHajCompanyResponse>> GetAll(ElmFilterRequest? request = null)
    {
        request ??= ElmFilterRequest.Create();

        request.AddSortColumn(ElmSortItem.CreateDesc(nameof(ElmCompanyResponse.TimeStamp).ToLower()));

        return GetLookups<List<ElmDhcHajCompanyResponse>>(request);
    }
 }
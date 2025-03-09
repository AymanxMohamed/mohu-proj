using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Clients;

internal class ElmInformationCenterDhcHajCompaniesClient(IElmInformationCenterClient client)
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: $"{LookupsConstants.MainCollectionName}/dhc-haj-companies",
            client),
        IElmInformationCenterDhcHajCompaniesClient
{
    public ErrorOr<List<ElmDhcHajCompanyResponse>> GetAll(ElmFilterRequest? request = null)
    {
        request ??= ElmFilterRequest.Create();

        request.AddSortColumn(ElmSortItem.CreateDesc(nameof(ElmCompanyResponse.TimeStamp).ToLower()));

        return GetLookups<List<ElmDhcHajCompanyResponse>>(request);
    }
 }
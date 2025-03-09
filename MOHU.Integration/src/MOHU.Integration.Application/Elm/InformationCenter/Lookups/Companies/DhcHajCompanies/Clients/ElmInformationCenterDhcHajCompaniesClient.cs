using MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.SpcCompanies.Clients;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.DhcHajCompanies.Clients;

internal class ElmInformationCenterDhcHajCompaniesClient(IElmInformationCenterClient client)
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: $"{LookupsConstants.MainCollectionName}/dhc-haj-companies",
            client),
        IElmInformationCenterDhcHajCompaniesClient
{
    public ErrorOr<List<ElmDhcHajCompanyResponse>> GetAll(ElmFilterRequest? request = null) =>
        GetLookups<List<ElmDhcHajCompanyResponse>>(request);
}
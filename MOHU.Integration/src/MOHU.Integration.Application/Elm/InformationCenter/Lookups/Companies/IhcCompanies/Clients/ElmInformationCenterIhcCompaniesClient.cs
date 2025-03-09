using MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.IhcCompanies.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.IhcCompanies.Clients;

internal class ElmInformationCenterIhcCompaniesClient(IElmInformationCenterClient client)
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: $"{LookupsConstants.MainCollectionName}/ihc-companies",
            client),
        IElmInformationCenterIhcCompaniesClient
{
    public ErrorOr<List<ElmIhcCompanyResponse>> GetAll(ElmFilterRequest? request = null) =>
        GetLookups<List<ElmIhcCompanyResponse>>(request);
}
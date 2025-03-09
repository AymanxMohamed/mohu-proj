using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.IhcCompanies.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.IhcCompanies.Clients;

internal class ElmInformationCenterIhcCompaniesClient(IElmInformationCenterClient client)
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: $"{LookupsConstants.MainCollectionName}/ihc-haj-companies",
            client),
        IElmInformationCenterIhcCompaniesClient
{
    public ErrorOr<List<ElmIhcCompanyResponse>> GetAll(ElmFilterRequest? request = null)
    {
        request ??= ElmFilterRequest.Create();

        request.AddSortColumn(ElmSortItem.CreateDesc(nameof(ElmCompanyResponse.TimeStamp).ToLower()));

        return GetLookups<List<ElmIhcCompanyResponse>>(request);
    }
}
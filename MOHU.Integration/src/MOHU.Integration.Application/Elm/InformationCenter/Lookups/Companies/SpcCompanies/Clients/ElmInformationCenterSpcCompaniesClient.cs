using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.SpcCompanies.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.SpcCompanies.Clients;

internal class ElmInformationCenterSpcCompaniesClient(
    IElmInformationCenterClient client,
    ElmInformationCenterApiSettings settings)
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: "spc-companies",
            client,
            settings),
        IElmInformationCenterSpcCompaniesClient
{
    public ErrorOr<List<ElmSpcCompanyResponse>> GetAll(ElmFilterRequest? request = null)
    {
        request ??= ElmFilterRequest.Create();

        request.AddSortColumn(ElmSortItem.CreateDesc(nameof(ElmCompanyResponse.TimeStamp).ToLower()));

        return GetLookups<List<ElmSpcCompanyResponse>>(request);
    }
}
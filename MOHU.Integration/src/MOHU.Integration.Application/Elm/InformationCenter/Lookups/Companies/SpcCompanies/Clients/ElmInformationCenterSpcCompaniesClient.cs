using MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.SpcCompanies.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.SpcCompanies.Clients;

internal class ElmInformationCenterSpcCompaniesClient(IElmInformationCenterClient client)
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: $"{LookupsConstants.MainCollectionName}/spc-companies",
            client),
        IElmInformationCenterSpcCompaniesClient
{
    public ErrorOr<List<ElmSpcCompanyResponse>> GetAll(ElmFilterRequest? request = null) =>
        GetLookups<List<ElmSpcCompanyResponse>>(request);
}
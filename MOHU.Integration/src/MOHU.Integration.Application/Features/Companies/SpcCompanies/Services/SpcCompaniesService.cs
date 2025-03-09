using MOHU.Integration.Application.Elm.InformationCenter.Common.Services;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.SpcCompanies.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.SpcCompanies.Dtos.Responses;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Constants;

namespace MOHU.Integration.Application.Features.Companies.SpcCompanies.Services;

public class SpcCompaniesService(
    IElmInformationCenterSpcCompaniesClient client,
    IConfigurationService configurationService,
    ICrmContext crmContext) : ElmSyncService<IElmInformationCenterSpcCompaniesClient, ElmSpcCompanyResponse, Company>(
        configurationService,
        client,
        crmContext,
        CompaniesConstants.LogicalName,
        Company.Create,
        x => x.ToCrmEntity(),
        (x, y) => x == y),
    ISpcCompaniesService;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Services;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.HajMissionsCompanies.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.HajMissionsCompanies.Dtos.Responses;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Constants;

namespace MOHU.Integration.Application.Features.Companies.HajMissions.Services;

public class HajMissionCompaniesService(
    IElmInformationCenterHajMissionCompaniesClient client,
    IConfigurationService configurationService,
    ICrmContext crmContext) : ElmSyncService<IElmInformationCenterHajMissionCompaniesClient, ElmHajMissionCompanyResponse, Company>(
        configurationService,
        client,
        crmContext,
        CompaniesConstants.LogicalName,
        Company.Create,
        x => x.ToCrmEntity(),
        (x, y) => x == y),
    IHajMissionCompaniesService;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Services;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Houses.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Houses.Dtos.Responses;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Constants;

namespace MOHU.Integration.Application.Features.Companies.Houses.Services;

public class HousesService(
    IElmInformationCenterHousesClient client,
    IConfigurationService configurationService,
    ICrmContext crmContext) : ElmSyncService<IElmInformationCenterHousesClient, ElmHouseResponse, Company>(
        configurationService,
        client,
        crmContext,
        CompaniesConstants.LogicalName,
        Company.Create,
        x => x.ToCrmEntity(),
        (x, y) => x == y),
    IHousesService;
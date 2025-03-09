using Common.Crm.Infrastructure.Factories;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Services;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.IhcCompanies.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.IhcCompanies.Dtos.Responses;
using MOHU.Integration.Domain.Features.Companies;
using MOHU.Integration.Domain.Features.Companies.Constants;
using MOHU.Integration.Domain.Features.Companies.Enums;

namespace MOHU.Integration.Application.Features.Companies.IhcCompanies.Services;

public class IhcCompaniesService(
    IElmInformationCenterIhcCompaniesClient client,
    IConfigurationService configurationService,
    ICrmContext crmContext) : ElmSyncService<IElmInformationCenterIhcCompaniesClient, ElmIhcCompanyResponse, Company>(
        configurationService,
        client,
        crmContext,
        CompaniesConstants.LogicalName,
        Company.Create,
        x => x.ToCrmEntity(),
        (x, y) => x == y),
    IIhcCompaniesService;
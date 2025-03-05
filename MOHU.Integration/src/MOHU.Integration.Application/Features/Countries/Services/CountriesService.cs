using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Services;
using MOHU.Integration.Domain.Features.Countries;
using MOHU.Integration.Domain.Features.Countries.Constants;

namespace MOHU.Integration.Application.Features.Countries.Services;

public class CountriesService(
    IElmInformationCenterCountriesClient client,
    IConfigurationService configurationService,
    ICrmContext crmContext) : ElmSyncService<IElmInformationCenterCountriesClient, ElmCountryResponse, Country>(
        configurationService, 
        client, 
        crmContext, 
        CountriesConstants.LogicalName,
        Country.Create,
        x => x.ToCrmEntity(),
        (x, y) => x == y), 
    ICountriesService;
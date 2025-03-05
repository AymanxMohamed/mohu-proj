using MOHU.Integration.Application.Elm.InformationCenter.Common.Constants;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants;
using MOHU.Integration.Application.Elm.InformationCenter.Services;
using MOHU.Integration.Domain.Features.Individuals.Constants;
using Individual = MOHU.Integration.Domain.Features.Individuals.Individual;

namespace MOHU.Integration.Application.Features.Individuals.Services;

public partial class IndividualsService(
    IElmInformationCenterApplicantDataClient client,
    IConfigurationService configurationService,
    ICrmContext crmContext) : ElmSyncService<IElmInformationCenterApplicantDataClient, ElmApplicant, Individual>(
        configurationService, 
        client, 
        crmContext, 
        IndividualConstants.LogicalName,
        Individual.Create), 
    IIndividualsService;
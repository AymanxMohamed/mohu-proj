using Common.Crm.Infrastructure.Factories;
using Common.Crm.Infrastructure.Repositories.Interfaces;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;

namespace MOHU.Integration.Application.Features.Individuals.Services;

public partial class IndividualsService(
    IElmInformationCenterApplicantDataClient client,
    IConfigurationService configurationService,
    ICrmContext crmContext) : IIndividualsService
{
    private readonly IGenericRepository _genericRepository =
        GenericRepositoriesFactory.CreateGenericRepository(crmContext.ServiceClient);
}
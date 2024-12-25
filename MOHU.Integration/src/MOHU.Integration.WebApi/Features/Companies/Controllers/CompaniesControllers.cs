using MOHU.Integration.Contracts.Companies.Services;

namespace MOHU.Integration.WebApi.Features.Companies.Controllers;


[Route("api/companies")]
public class CompaniesControllers(ICompaniesService service) : BaseController;
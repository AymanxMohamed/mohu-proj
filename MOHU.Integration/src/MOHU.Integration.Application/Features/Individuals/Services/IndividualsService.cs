using Individual = MOHU.Integration.Domain.Features.Individuals.Individual;

namespace MOHU.Integration.Application.Features.Individuals.Services;

public interface IIndividualsService
{
    List<Individual> GetByElmReferenceIds(List<int>? ids);
    
    Task<List<Individual>> SyncWithElm();
}
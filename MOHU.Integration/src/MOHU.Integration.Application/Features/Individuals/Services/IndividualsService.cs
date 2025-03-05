using MOHU.Integration.Application.Elm.InformationCenter.Services;
using Individual = MOHU.Integration.Domain.Features.Individuals.Individual;

namespace MOHU.Integration.Application.Features.Individuals.Services;

public interface IIndividualsService : IElmSyncService<Individual>;
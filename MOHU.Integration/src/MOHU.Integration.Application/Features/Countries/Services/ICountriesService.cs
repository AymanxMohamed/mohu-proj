using MOHU.Integration.Application.Elm.InformationCenter.Common.Services;
using MOHU.Integration.Domain.Features.Countries;

namespace MOHU.Integration.Application.Features.Countries.Services;

public interface ICountriesService : IElmSyncService<Country>;
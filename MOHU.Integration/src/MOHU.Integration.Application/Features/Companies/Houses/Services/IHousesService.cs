using MOHU.Integration.Application.Elm.InformationCenter.Common.Services;
using MOHU.Integration.Domain.Features.Companies;

namespace MOHU.Integration.Application.Features.Companies.Houses.Services;

public interface IHousesService : IElmSyncService<Company>;
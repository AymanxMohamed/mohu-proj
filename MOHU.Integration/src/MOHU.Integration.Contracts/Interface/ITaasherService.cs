using MOHU.Integration.Contracts.Dto.Taasher;

namespace MOHU.Integration.Contracts.Interface;

public interface ITaasherService
{
    Task<bool> UpdateStatusAsync(TaasherUpdateStatusRequest request);
}
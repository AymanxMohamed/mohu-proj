using MOHU.Integration.Contracts.Dto.Sahab;

namespace MOHU.Integration.Contracts.Interface;

public interface ISahabService
{
    Task<bool> UpdateStatusAsync(SahabUpdateStatusRequest request);
}
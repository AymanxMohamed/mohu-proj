using MOHU.Integration.Contracts.Dto;

namespace MOHU.Integration.Contracts.Interface
{
    public interface ITaasherService
    {
        Task<bool> UpdateStatus(UpdateStatusRequest request);
    }
}

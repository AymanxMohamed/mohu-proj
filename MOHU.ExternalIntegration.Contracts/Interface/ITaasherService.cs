using MOHU.ExternalIntegration.Contracts.Dto;

namespace MOHU.ExternalIntegration.Contracts.Interface
{
    public interface ITaasherService
    {
        Task UpdateStatus(UpdateStatusRequest request);
    }
}

using MOHU.ExternalIntegration.Contracts.Dto;

namespace MOHU.ExternalIntegration.Contracts.Interface
{
    public interface IKedanaService
    {
        Task UpdateStatus(UpdateStatusRequest request);
    }
}

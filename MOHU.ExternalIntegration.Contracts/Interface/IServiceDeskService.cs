using MOHU.ExternalIntegration.Contracts.Dto;

namespace MOHU.ExternalIntegration.Contracts.Interface
{
    public interface IServiceDeskService
    {
        Task UpdateStatus(UpdateStatusRequest request);
    }
}

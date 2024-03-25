using MOHU.Integration.Contracts.Dto;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IServiceDeskService
    {
        Task<bool> UpdateStatus(UpdateStatusRequest request);

    }
}

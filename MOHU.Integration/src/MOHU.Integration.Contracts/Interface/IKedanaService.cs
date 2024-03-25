using MOHU.Integration.Contracts.Dto;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IKedanaService
    {
        Task<bool> UpdateStatus(UpdateStatusRequest request);

    }
}

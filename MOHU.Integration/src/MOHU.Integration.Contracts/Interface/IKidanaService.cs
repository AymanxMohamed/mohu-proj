using MOHU.Integration.Contracts.Dto;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IKidanaService
    {
        Task<bool> UpdateStatus(UpdateStatusRequest request);

    }
}

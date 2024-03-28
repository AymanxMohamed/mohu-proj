using MOHU.Integration.Contracts.Dto.Kidana;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IKidanaService
    {
        Task<bool> UpdateStatusAsync(KidanaUpdateStatusRequest request);

    }
}

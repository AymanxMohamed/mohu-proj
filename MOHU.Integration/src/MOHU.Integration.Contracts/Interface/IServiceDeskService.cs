using MOHU.Integration.Contracts.Dto.ServiceDesk;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IServiceDeskService
    {
        Task<bool> UpdateStatusAsync(ServiceDeskUpdateStatusRequest request);

        Task<Guid> GetTicketBySdNumber(string sdNumber);
    }
}

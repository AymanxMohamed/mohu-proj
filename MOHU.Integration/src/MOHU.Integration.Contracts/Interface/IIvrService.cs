using MOHU.Integration.Contracts.Dto.Ivr;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IIvrService
    {
        Task<string> GetCustomerProfileUrlAsync(string mobileNumber);
        Task<Guid> CreatePhoneCall(CreatePhoneCallRequest request);

    }
}

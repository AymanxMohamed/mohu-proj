using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.CreateProfile;

namespace MOHU.Integration.Contracts.Interface.Customer
{
    public interface ICustomerService
    {
        Task<Guid> CreateProfileAsync(CreateProfileRequest model);
        Task<LookupDto> CreateProfilelAsync(string mobileNumber);
        Task<LookupDto?> GetIndividualByMobileNumberAsync(string mobileNumber);
    }
}

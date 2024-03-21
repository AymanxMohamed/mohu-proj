using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.CreateProfile;

namespace MOHU.Integration.Contracts.Interface.Customer
{
    public interface ICustomerService
    {
        Task<Guid> CreateProfile(CreateProfileRequest model);
        Task<LookupDto?> GetIndividualByMobileNumberAsync(string mobileNumber);
        Task<LookupDto> CreateIndividualAsync(string mobileNumber);
    }
}

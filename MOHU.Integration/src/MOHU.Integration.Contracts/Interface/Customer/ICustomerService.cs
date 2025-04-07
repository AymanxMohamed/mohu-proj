using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Dto.Hootsuite;

namespace MOHU.Integration.Contracts.Interface.Customer
{
    public interface ICustomerService
    {
        Task<Guid> CreateProfileAsync(CreateProfileRequest model);
        Task<LookupDto> CreateProfilelAsync(string mobileNumber);
        Task<LookupDto?> GetIndividualByMobileNumberAsync(string mobileNumber);
        Task<Guid?> FindOrCreateProfileAsync(ContactProfileDto contactProfile);
        Task<Guid?> FindExistingCustomerAsync(string? mobileNumber, string? email, string? idNumber);
        Task<GetProfileResponse> CreateMinimalProfileAsync(string? mobileNumber, string? email, string? idNumber, string? firstName, string? lastName);
        Task<GetProfileResponse> FindOrCreateCustomerAsync(string? internationalMobile, string? email, string? idNumber, string? firstName, string? lastName);
    }
}

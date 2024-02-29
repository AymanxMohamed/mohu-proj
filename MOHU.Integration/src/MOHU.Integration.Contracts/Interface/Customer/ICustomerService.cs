using MOHU.Integration.Contracts.Dto.CreateProfile;

namespace MOHU.Integration.Contracts.Interface.Customer
{
    public interface ICustomerService
    {
        Task<Guid> CreateProfile(CreateProfileResponse model);
    }
}

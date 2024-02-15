using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Dto.Common;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IIndividualService
    {
        Task<LookupDto?> GetIndividualByMobileNumberAsync(string mobileNumber);
        Task<LookupDto> CreateIndividualAsync(string mobileNumber);
    }
}

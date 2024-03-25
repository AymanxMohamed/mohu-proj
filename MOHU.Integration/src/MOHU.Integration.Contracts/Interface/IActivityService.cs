using MOHU.Integration.Contracts.Dto.Activity;
using MOHU.Integration.Contracts.Dto.Common;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IActivityService
    {
        Task<LookupDto> CreateActivityAsync(CreateActivityRequest request);
    }
}

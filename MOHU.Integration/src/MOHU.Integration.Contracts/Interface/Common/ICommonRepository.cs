using MOHU.Integration.Contracts.Dto.Common;

namespace MOHU.Integration.Contracts.Interface.Common
{
    public interface ICommonRepository
    {
        Task<IEnumerable<OptionDto>> GetOptionSet(string entityName, string optionSetName, string language);
        Task<IEnumerable<LookupValueDto>> GetLookups(string entityName, string language);
    }
}

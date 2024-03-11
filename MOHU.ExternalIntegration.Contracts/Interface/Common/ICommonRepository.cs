using MOHU.ExternalIntegration.Contracts.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Interface.Common
{
    public interface ICommonRepository
    {
        Task<IEnumerable<OptionDto>> GetOptionSet(string entityName, string optionSetName, string language);
        Task<IEnumerable<LookupValueDto>> GetLookups(string entityName, string language);
    }
}

using MOHU.ExternalIntegration.Contracts.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Interface.Common
{
    public interface IMessageService
    {
        Task<MessageDto> GetMessageByCodeAsync(string code);
    }
}

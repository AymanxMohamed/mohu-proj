using MOHU.Integration.Contracts.Dto.Hootsuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Interface;
public interface IHootsuiteService
{
    Task<Guid?> ConversationResolved(ConversationResolvedRequest conversationResolvedRequest);
}

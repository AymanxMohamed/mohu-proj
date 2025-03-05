using MOHU.Integration.WebApi.Features.Hootsuite.Webhooks.ConversationResolved;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.Hootsuite;
public class ConversationResolvedRequest : ContactProfileDto
{
    public List<Note > Notes { get; set; } = new List<Note>();
    public List<Topic> Categories { get; set; } = new List<Topic>();
}

using Microsoft.Xrm.Sdk.Query;
using MOHU.ExternalIntegration.Contracts.Dto.Common;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Contracts.Interface.Common;
using MOHU.ExternalIntegration.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Application.common
{
    public class MessageService : IMessageService
    {
        private readonly ICrmContext _crmContext;
        public MessageService(ICrmContext crmContext)
        {
            _crmContext = crmContext;
        }

        public async Task<MessageDto> GetMessageByCodeAsync(string code)
        {
            var msgQuery = new QueryExpression(ldv_message.EntityLogicalName)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(ldv_message.Fields.ldv_englishmessage)
            };
            msgQuery.Criteria.AddCondition(new ConditionExpression(ldv_message.Fields.ldv_code, ConditionOperator.Equal, code));
            var entityCollection = await _crmContext.ServiceClient.RetrieveMultipleAsync(msgQuery);
            return new MessageDto
            {
                Code = code,
                ErrorMessage = entityCollection?.Entities?.FirstOrDefault()?.GetAttributeValue<string>(ldv_message.Fields.ldv_englishmessage)
            };
        }







    }
}

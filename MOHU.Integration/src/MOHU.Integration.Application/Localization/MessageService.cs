﻿using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Application.Localization
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
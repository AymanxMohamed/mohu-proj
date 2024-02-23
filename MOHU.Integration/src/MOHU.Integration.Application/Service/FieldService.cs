using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Dto.Field;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Domain.Entitiy;
using MOHU.Integration.Domain.Enum;
using MOHU.Integration.Shared;

namespace MOHU.Integration.Application.Service
{
    public class FieldService : IFieldService
    {
        private readonly ICrmContext _crmContext;

        public FieldService(ICrmContext crmContext)
        {
            _crmContext = crmContext;
        }

        public async Task<IEnumerable<FieldDto>> GetFieldsBySubCategoryAsync(string subCategoryId)
        {
            var result = new List<FieldDto>();
            var query = GetFieldsQueryForSubCategoryId(subCategoryId);

            var fieldEntities = (await _crmContext.ServiceClient.RetrieveMultipleAsync(query)).Entities;

            foreach (var field in fieldEntities)           
                result.Add(await MapField(field));

            return result;
        }
        private QueryExpression GetFieldsQueryForSubCategoryId(string subCategoryId)
        {
            var query = new QueryExpression(ldv_categoryfields.EntityLogicalName)
            {
                NoLock = true
            };

            var andFilter = new FilterExpression(LogicalOperator.And);
            query.Criteria.AddFilter(andFilter);
            andFilter.AddCondition(ldv_categoryfields.Fields.ldv_subcategoryid, ConditionOperator.Equal, subCategoryId);
            andFilter.AddCondition(ldv_categoryfields.Fields.ldv_showonportalcode,ConditionOperator.In, (int)ShowOnPortal.Both, (int)ShowOnPortal.Portal);

            var fieldLink = query.AddLink(ldv_field.EntityLogicalName, ldv_categoryfields.Fields.ldv_fieldid, ldv_field.Fields.Id);
            fieldLink.EntityAlias = Globals.LinkEntityConsts.FieldEntityLink;

            fieldLink.Columns.AddColumns(
               ldv_field.Fields.ldv_displaynamear,
               ldv_field.Fields.ldv_displaynameen,
               ldv_field.Fields.ldv_entitylookuplogicalname,
               ldv_field.Fields.ldv_fieldschemaname,
               ldv_field.Fields.ldv_regexpression,
               ldv_field.Fields.ldv_typecode
                );

            var messageLink = fieldLink.AddLink(ldv_message.EntityLogicalName, ldv_field.Fields.ldv_messageid, ldv_message.Fields.Id, JoinOperator.LeftOuter);
            messageLink.EntityAlias = Globals.LinkEntityConsts.MessageEntityLink;

            messageLink.Columns.AddColumns(ldv_message.Fields.ldv_arabicmessage,ldv_message.Fields.ldv_englishmessage);

            return query;

        }
        private async Task<FieldDto> MapField(Entity entity)
        {
            var field = new FieldDto()
            {
                Mandatory = entity.GetAttributeValue<bool>(ldv_categoryfields.Fields.ldv_mandatorycode),
                PortalDisplayOrder = Convert.ToInt32(entity.GetAttributeValue<string>(ldv_categoryfields.Fields.ldv_portaldisplayorder)),
            };
            if (entity.Attributes.ContainsKey($"{Globals.LinkEntityConsts.FieldEntityLink}.{ldv_field.Fields.Id}"))
                field.Id = entity.GetAttributeValue<string>($"{Globals.LinkEntityConsts.FieldEntityLink}.{ldv_field.Fields.Id}");

            if (entity.Attributes.ContainsKey($"{Globals.LinkEntityConsts.FieldEntityLink}.{ldv_field.Fields.ldv_regexpression}"))
                field.Regex = entity.GetAttributeValue<string>($"{Globals.LinkEntityConsts.FieldEntityLink}.{ldv_field.Fields.ldv_regexpression}");

            if (entity.Attributes.ContainsKey($"{Globals.LinkEntityConsts.MessageEntityLink}.{ldv_message.Fields.ldv_arabicmessage}"))
                field.RegexErrorMessage = entity.GetAttributeValue<string>($"{Globals.LinkEntityConsts.MessageEntityLink}.{ldv_message.Fields.ldv_arabicmessage}");

            //field.Name
            return field;
        }
    }
}

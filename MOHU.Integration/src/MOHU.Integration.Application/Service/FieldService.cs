using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Dto.Field;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Domain.Entitiy;
using MOHU.Integration.Domain.Enum;

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
            var entityCollection = new List<Entity>();

            var query = new QueryExpression(ldv_field.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(
               ldv_field.Fields.ldv_fieldId,
               ldv_field.Fields.ldv_fieldschemaname,
               ldv_field.Fields.ldv_displaynamear,
               ldv_field.Fields.ldv_displaynameen,
               ldv_field.Fields.ldv_ismandatory,
               ldv_field.Fields.ldv_regexpression,
               ldv_field.Fields.ldv_typecode,
               ldv_field.Fields.ldv_Xmlquery,
               ldv_field.Fields.StateCode,
               ldv_field.Fields.ldv_messageid,
               ldv_field.Fields.ldv_validationquery,
               ldv_field.Fields.ldv_calldependentfields)
            };

            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(
                new ConditionExpression(ldv_field.Fields.StateCode, ConditionOperator.Equal, 0));
            query.Criteria.AddFilter(filter);

            var messageLink = query.AddLink(ldv_message.EntityLogicalName,
                                            ldv_message.Fields.ldv_messageId,
                                            ldv_field.Fields.ldv_messageid, JoinOperator.LeftOuter);

            messageLink.Columns.AddColumns(ldv_message.Fields.ldv_arabicmessage, ldv_message.Fields.ldv_englishmessage);



            var categoryFieldsLink = query.AddLink(ldv_categoryfields.EntityLogicalName, ldv_categoryfields.Fields.ldv_fieldid, ldv_categoryfields.Fields.ldv_fieldid);

            categoryFieldsLink.Columns.AddColumns(ldv_categoryfields.Fields.ldv_mandatorycode, ldv_categoryfields.Fields.ldv_showonportalcode, ldv_categoryfields.Fields.ldv_portaldisplayorder);
            categoryFieldsLink.LinkCriteria.FilterOperator = LogicalOperator.And;
            categoryFieldsLink.LinkCriteria.AddCondition(ldv_categoryfields.Fields.ldv_subcategoryid, ConditionOperator.Equal, subCategoryId);
            categoryFieldsLink.LinkCriteria.AddCondition(ldv_categoryfields.Fields.ldv_showonportalcode, ConditionOperator.In, (int)ShowOnPortal.Both, (int)ShowOnPortal.Portal);


            categoryFieldsLink.EntityAlias = "categoryfieldsLink";


            var collRecords = (await _crmContext.ServiceClient.RetrieveMultipleAsync(query)).Entities;

            entityCollection.AddRange(collRecords);

            throw new NotImplementedException();
        }
    }
}

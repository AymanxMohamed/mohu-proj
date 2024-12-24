using Common.Crm.Domain.Common.ColumnSets.Constants;
using Common.Crm.Domain.Common.OptionSets.Constants;

namespace Common.Crm.Domain.Common.Extensions
{
    public static partial class EntityExtensions
    {
        public static OptionSetValue GetOptionSetValue(this Entity entity, string optionSetName)
        {
            return entity.GetAttributeValue<OptionSetValue>(optionSetName);
        }
        
        public static bool IsActive(this Entity entity)
        {
            return entity.GetOptionSetValue(ColumnSetConstants.Status).Value 
                   == OptionSetConstants.Status.Active.Value;
        }
        
        public static bool IsDeactivated(this Entity entity)
        {
            return !entity.IsActive();
        }
        
        public static string GetPrimaryKey(this Entity entity)
        {
            return  $"{entity.LogicalName}id";
        }
    }
    
}
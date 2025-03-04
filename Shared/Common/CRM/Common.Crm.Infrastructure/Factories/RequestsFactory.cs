using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;

namespace Common.Crm.Infrastructure.Factories
{
    public static class RequestsFactory
    {
        public static ExecuteTransactionRequest CreateExecuteTransactionRequest(
            OrganizationRequestCollection? requests = null)
        {
            return new ExecuteTransactionRequest
            {
                Requests = requests ?? []
            };
        }

        public static CreateRequest CreateCreateRequest(Entity target)
        {
            return new CreateRequest { Target = target };
        }
        
        public static UpdateRequest CreateUpdateRequest(Entity target)
        {
            return new UpdateRequest
            {
                Target = target
            };
        }
        
        public static DeleteRequest CreateDeleteRequest(Entity target)
        {
            return new DeleteRequest { Target = target.ToEntityReference() };
        }

        public static SetStateRequest CreateSetStateRequest(
            Entity entity, 
            OptionSetValue status, 
            OptionSetValue statusReason)
        {
            return new SetStateRequest
            {   
                EntityMoniker = entity.ToEntityReference(),
                State = status,
                Status = statusReason
            };
        }

        public static OrganizationRequestCollection CreateCreateRequestCollection(
            IEnumerable<Entity>? entities = null, 
            params Entity[] restEntities)
        {
            if (entities is null && restEntities.Length == 0)
                return [];
            
            var requestCollection = new OrganizationRequestCollection();
            requestCollection.AddRange(restEntities.Select(CreateCreateRequest));
            
            if (entities != null)
                requestCollection.AddRange(entities.Select(CreateCreateRequest));
            
            return requestCollection;
        }
        
        public static OrganizationRequestCollection? CreateCreateRequestCollection(Entity? entity)
        {
            return entity is null ? null : [CreateCreateRequest(entity)];
        }
    }
}
using Microsoft.Xrm.Sdk;
using MOHU.Integration.Contracts.Dto.Activity;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Application.Service
{
    public class ActivityService : IActivityService
    {
        private readonly ICrmContext _crmContext;

        public ActivityService(ICrmContext crmContext)
        {
            _crmContext = crmContext;
        }

        public async Task<LookupDto> CreateActivityAsync(CreateActivityRequest request)
        {
            var activity = new Entity(request.ActivityName);
            if(request.From is not null)
            {
                var fromActivityParty = new Entity(ActivityParty.EntityLogicalName);
                fromActivityParty.Attributes.Add(ActivityParty.Fields.PartyId, new EntityReference(request.From.EntityLogicalName, request.From.Id));
                var from = new Entity[] { fromActivityParty };
                activity.Attributes.Add("from", from);
            }
           if(request.To is not null && request.To.Any())
            {
                var toActivityParties = new List<Entity>();
                foreach (var to in request.To)
                {
                    var toActivityParty = new Entity(ActivityParty.EntityLogicalName);
                    toActivityParty.Attributes.Add(ActivityParty.Fields.PartyId, new EntityReference(to.EntityLogicalName, to.Id));
                    toActivityParties.Add(toActivityParty);
                }
                activity.Attributes.Add("to", toActivityParties.ToArray());
            }
            activity.Attributes.Add("ownerid", new EntityReference(request.Owner.EntityLogicalName, request.Owner.Id));
            
            foreach (var attribute in request.ExtraProperties)           
                activity.Attributes.Add(attribute.Key, attribute.Value);
            
            var activityId = await _crmContext.ServiceClient.CreateAsync(activity);

            return new LookupDto { Id = activityId, EntityLogicalName = request.ActivityName };
        }
    }
}

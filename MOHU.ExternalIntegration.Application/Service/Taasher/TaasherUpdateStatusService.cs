using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.ExternalIntegration.Application.Exceptions;
using MOHU.ExternalIntegration.Contracts.Dto.Taasher;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Application.Service.Taasher
{
    public class TaasherUpdateStatusService : ITaasherUpdateStatusService
    {

        private readonly ICrmContext crmContext;
        private readonly ICommonMethod _commonMethod;
       
        public TaasherUpdateStatusService(ICrmContext crmContext, ICommonMethod commonMethod)
        {
            this.crmContext = crmContext;
            _commonMethod = commonMethod;
        }

        public async Task<bool> TaasherUpdateStatus(TaasherUpdateStatusResponse model)
        {
            //throw new NotFoundException();

            //  var isCustExist = await CheckCustomerExist(model.CustId);

            if (model.CustId == Guid.Empty)
            {
                throw new NotFoundException("Customer Id is Required ");
            }
            if (model.TicketId == Guid.Empty)
            {
                throw new NotFoundException("Ticket Id is Required ");
            }

            var TicketidExist = await _commonMethod.CheckTicketIdExist(model.TicketId);

            var isCustExist = await _commonMethod.CheckCustomerExist(model.CustId);
            if (!isCustExist)
            {
                throw new NotFoundException("This Customer is not Exist  ");
            }
            var query = new QueryExpression()
            {
                EntityName = Incident.EntityLogicalName,
                NoLock = true
            };

            if (TicketidExist == true)
            {

                var Ticket = new Entity(Incident.EntityLogicalName)
                {
                    Id = model.TicketId
                };
                Ticket.Attributes.Add(Incident.Fields.IntegrationClosureReason, model.Resolution);

                Ticket.Attributes.Add(Incident.Fields.IntegrationClosureDate, model.ResolutionDate);

                Ticket.Attributes.Add(Incident.Fields.IntegrationStatus,
                  new OptionSetValue(Convert.ToInt32(model.IntegrationStatus)));


               // Ticket.Attributes.Add(Incident.Fields.IsTashirUpdated, model.IsTashirUpdated);
                Ticket.Attributes.Add(Incident.Fields.IsTashirUpdated, true);



                crmContext.ServiceClient.Update(Ticket);

                return true;

            }
            return false;

        }

      



    }
}

using Microsoft.Extensions.Localization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.ExternalIntegration.Application.Exceptions;
using MOHU.ExternalIntegration.Contracts.Dto;
//using MOHU.ExternalIntegration.Contracts.Dto.ServiceDisk;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Domain.Entitiy;
using MOHU.ExternalIntegration.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Application.Service.ServiceDesk
{
    public class ServiceDeskUpdateStatusService : IUpdateStatusService          /*IServiceDiskService*/

    {
        private readonly ICrmContext crmContext;
        private readonly ICommonMethod _commonMethod;

        private readonly IStringLocalizer _localizer;
        public ServiceDeskUpdateStatusService(ICrmContext crmContext, ICommonMethod commonMethod , IStringLocalizer localizer)
        {
            this.crmContext = crmContext;

            _commonMethod = commonMethod;
            _localizer = localizer;
        }

        public async Task<bool> UpdateStatus(UpdateStatusRequest model)
        {

            if (model.CustId == Guid.Empty)  
            {
                throw new NotFoundException(_localizer[ErrorMessageCodes.CustomerIdRquired]);
            }
            if (model.TicketId == Guid.Empty)
            {
                throw new NotFoundException(_localizer[ErrorMessageCodes.TicketIdisRequired]);
            }

            var TicketidExist = await _commonMethod.CheckTicketIdExist(model.TicketId);

            var isCustExist = await _commonMethod.CheckCustomerExist(model.CustId);
            if (!isCustExist)
            {
                throw new NotFoundException(_localizer[ErrorMessageCodes.CustomerExist]);
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

               
                Ticket.Attributes.Add(Incident.Fields.IsServiceDeskUpdated, true);

                crmContext.ServiceClient.Update(Ticket);

                return true;

            }
            return false;

        }


    }
}

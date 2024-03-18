using Microsoft.Extensions.Localization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Dto;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Domain.Entitiy;
using MOHU.Integration.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Service.Taasher
{
    public class TasherUpdateStatusService : IUpdateStatusService
    {

        private readonly ICrmContext crmContext;
       
        private readonly IStringLocalizer _localizer;
        private readonly IHelperMethod _HelperMethod;
        public TasherUpdateStatusService(ICrmContext crmContext, IHelperMethod HelperMethod, IStringLocalizer localizer)
        {
            this.crmContext = crmContext;
            _HelperMethod = HelperMethod;
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

            var TicketidExist = await _HelperMethod.CheckTicketIdExist(model.TicketId);

            var isCustExist = await _HelperMethod.CheckCustomerExist(model.CustId);
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




                Ticket.Attributes.Add(Incident.Fields.IsTashirUpdated, true);

                crmContext.ServiceClient.Update(Ticket);

                return true;

            }
            return false;

        }






    }
}

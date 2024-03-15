using Microsoft.BusinessData.MetadataModel;
using Microsoft.Xrm.Sdk.Query;
using MOHU.ExternalIntegration.Application.Exceptions;
//using MOHU.ExternalIntegration.Contracts.Dto.Kedana;
using MOHU.ExternalIntegration.Contracts.Dto;
using MOHU.ExternalIntegration.Contracts.Interface;
using MOHU.ExternalIntegration.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Extensions.Localization;
using MOHU.ExternalIntegration.Shared;


namespace MOHU.ExternalIntegration.Application.Service.Kedana
{
    public class KedanaUpdateStatusService : IUpdateStatusService
    {
        private readonly ICrmContext crmContext;

        private readonly ICommonMethod _commonMethod;

        private readonly IStringLocalizer _localizer;
        public KedanaUpdateStatusService(ICrmContext crmContext,
            ICommonMethod commonMethod,
            IStringLocalizer localizer

            )

        {
            this.crmContext = crmContext;
            _commonMethod = commonMethod;
            _localizer= localizer;



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

                var Ticket = new Microsoft.Xrm.Sdk.Entity(Incident.EntityLogicalName)
                {
                    Id = model.TicketId
                };
               
                Ticket.Attributes.Add(Incident.Fields.IntegrationClosureReason, model.Resolution);
                Ticket.Attributes.Add(Incident.Fields.IntegrationClosureDate, model.ResolutionDate);
                Ticket.Attributes.Add(Incident.Fields.IntegrationStatus,
              new OptionSetValue(Convert.ToInt32(model.IntegrationStatus)));


               
                Ticket.Attributes.Add(Incident.Fields.IsKadanaUpdated, true);



                crmContext.ServiceClient.Update(Ticket);

                return true;

            }
            return false;

        }






    }
}

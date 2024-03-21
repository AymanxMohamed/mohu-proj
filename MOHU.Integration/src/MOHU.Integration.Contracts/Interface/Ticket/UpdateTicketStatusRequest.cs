using MOHU.Integration.Contracts.Dto;

namespace MOHU.Integration.Contracts.Interface.Ticket
{
    public class UpdateTicketStatusRequest : UpdateStatusRequest
    {
        public string FlagLogicalName { get; set; }
    }
}

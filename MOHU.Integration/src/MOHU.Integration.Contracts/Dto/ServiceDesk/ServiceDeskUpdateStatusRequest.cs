namespace MOHU.Integration.Contracts.Dto.ServiceDesk
{
    public class ServiceDeskUpdateStatusRequest
    {
        public string TicketNumber { get; set; }
        public string Resolution { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public IntegrationStatus IntegrationStatus { get; set; }

    }
}

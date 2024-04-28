namespace MOHU.Integration.Contracts.Dto.Taasher
{
    public class TaasherUpdateStatusRequest
    {
        public string TicketNumber { get; set; }
        public string Resolution { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public IntegrationStatus IntegrationStatus { get; set; }

    }
}

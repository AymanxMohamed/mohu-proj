namespace MOHU.Integration.Contracts.Dto.Kidana
{
    public class KidanaUpdateStatusRequest
    {
        public int TicketId{ get; set; }
        public string Resolution { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public IntegrationStatus IntegrationStatus { get; set; }

    }
}

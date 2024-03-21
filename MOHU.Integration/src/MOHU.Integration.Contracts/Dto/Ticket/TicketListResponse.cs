namespace MOHU.Integration.Contracts.Dto.Ticket
{
    public class TicketListResponse
    {
        public ICollection<TicketDto> Tickets { get; set; }
        public int TotalRecords { get; set; }
        public TicketListResponse()
        {
            Tickets = new List<TicketDto>();
        }
    }
}

namespace MOHU.Integration.Contracts.Dto.Ticket
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string TicketNumber { get; set; }
        public string TicketType { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public DateTime CreationOn { get; set; }
    }
}

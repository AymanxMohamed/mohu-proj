using MOHU.Integration.Contracts.Dto.Document;

namespace MOHU.Integration.Contracts.Dto.Ticket
{
    public class TicketDetailsResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string TicketNumber { get; set; }
        public string Resolution { get; set; }
        public string Status { get; set; }
        public string TicketType { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string SecondarySubCategory { get; set; }
        public string Location { get; set; }
        public string BeneficiaryType { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public ICollection<DocumentDto> Documents { get; set; }
        public TicketDetailsResponse()
        {
            Documents = new List<DocumentDto>();
        }
    }
}

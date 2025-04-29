using MOHU.Integration.Contracts.Dto.Document;

namespace MOHU.Integration.Contracts.Dto.Ticket;

public class TicketDetailsResponse
{
    public Guid Id { get; init; }
    public string Description { get; set; }
    public string TicketNumber { get; set; }
    public string Resolution { get; set; } // missing
    public string Status { get; set; }
    public string TicketType { get; set; }
    public string Category { get; set; }
    public string SubCategory { get; set; }
    public string SecondarySubCategory { get; set; }
    public string Location { get; set; } // misisng
    public string BeneficiaryType { get; set; } // misisng
    public DateTime CreatedOn { get; set; } // missing
    public DateTime? ResolutionDate { get; set; } // missing
    public ICollection<DocumentDto> Documents { get; set; } // misisng
    public TicketDetailsResponse()
    {
        Documents = new List<DocumentDto>();
    }
}
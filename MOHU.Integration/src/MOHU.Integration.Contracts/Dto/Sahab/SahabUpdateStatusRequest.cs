namespace MOHU.Integration.Contracts.Dto.Sahab;

public class SahabUpdateStatusRequest
{
    public string TicketNumber { get; set; } = null!;
    public string Resolution { get; set; } = null!;
    public DateTime? ResolutionDate { get; set; }
    public IntegrationStatus IntegrationStatus { get; set; }
}
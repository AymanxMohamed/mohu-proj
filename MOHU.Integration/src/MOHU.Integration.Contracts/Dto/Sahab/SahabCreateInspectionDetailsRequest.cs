using MOHU.Integration.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.Sahab;
public class SahabCreateInspectionDetailsRequest
{
    public string? CaseTicketNumber { get; set; }
    public string? CaseClosureReason { get; set; }
    public DateTime? CaseClosureDateTime { get; init; }
    public IntegrationStatus? CaseIntegrationStatus { get; init; } = IntegrationStatus.PendingOnInspection; // 1 : close , 8: Pending On Incpection
    public string? CaseStatusReason { get; set; }

    public int? CaseIntegrationType { get; init; }
    public string? InspectorName { get; set; }
    public string? Comment { get; set; }
    public DateTime? ActionDate { get; set; } = DateTime.Now;
    public SahabStatusEnum Status { get; set; }
    public string? Branch { get; set; }
    public string? Checklist { get; set; }
    public string? City { get; set; }
    public string? Department { get; set; }
    public string? District { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime? VisitDate { get; set; }
    public string? LicenseNumber { get; set; }
    public string? Region { get; set; }
    public string? Supervisor { get; set; }
    public string? TicketStatus { get; set; }
    public string? TicketAge { get; set; }
    public string? UpdatedLocation { get; set; }
    public string? VisitAge { get; set; }
    public string? VisitCategory { get; set; }
    public string? VisitCode { get; set; }
    public string? VisitStatus { get; set; }
    public string? VisitType { get; set; }
}
using MOHU.Integration.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.Sahab;
public class SahabCreateInspectionDetailsRequest
{
    public string CaseTicketNumber { get; set; } = string.Empty;
    public string? CaseClosureReason { get; set; } = string.Empty;
    public DateTime? CaseClosureDateTime { get; init; }
    public IntegrationStatus? CaseIntegrationStatus { get; init; } = IntegrationStatus.PendingOnInspection;// 1 : close 
    public int? CaseIntegrationType { get; init; }
    public string InspectorName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty; 
    public DateTime ActionDate { get; set; } = DateTime.Now;
    public SahabStatusEnum Status { get; set; }
    public string Branch { get; set; } = string.Empty;
    public string Checklist { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public DateTime EndTime { get; set; }
    public DateTime VisitDate { get; set; }
    public string LicenseNumber { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Supervisor { get; set; } = string.Empty;
    public string TicketStatus { get; set; } = string.Empty;
    public string TicketAge { get; set; } = string.Empty;
    public string UpdatedLocation { get; set; } = string.Empty;
    public string VisitAge { get; set; } = string.Empty;
    public string VisitCategory { get; set; } = string.Empty;
    public string VisitCode { get; set; } = string.Empty;
    public string VisitStatus { get; set; } = string.Empty;
    public string VisitType { get; set; } = string.Empty;
   
}

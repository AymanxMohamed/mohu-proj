using System.ComponentModel.DataAnnotations;

namespace MOHU.Integration.Domain.Features.Tickets.Enums;

public enum IntegrationStatus
{
    [Display(Name = "Close The Ticket")]
    CloseTheTicket = 1,
    [Display(Name = "Need More details")]
    NeedMoreDetails = 2, 
    [Display(Name = "Pending On Inspection")]
    PendingOnInspection = 8,
}
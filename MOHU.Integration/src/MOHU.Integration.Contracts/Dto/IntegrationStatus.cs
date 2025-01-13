using System.ComponentModel.DataAnnotations;

namespace MOHU.Integration.Contracts.Dto;

public enum IntegrationStatus
{
    [Display(Name = "Close The Ticket")]
    CloseTheTicket = 1,
    [Display(Name = "Need More details")]
    NeedMoreDetails = 2,
}
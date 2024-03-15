using System.ComponentModel.DataAnnotations;

namespace MOHU.ExternalIntegration.Contracts.Enum
{
    public enum IntegrationStatus
    {
        [Display(Name = "Close The Ticket")]
        CloseTheTicket = 1,
        [Display(Name = "Cancel")]
        Canceled = 2,
    }


}

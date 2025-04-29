using Common.Crm.Application.Common.Dtos.Responses;
using MOHU.Integration.Domain.Features.Tickets.Entities;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Dtos.Responses.DetailsResponse;

public class NusukMasarTicketCustomerInformation
{
    private NusukMasarTicketCustomerInformation(TicketCustomerInformation customerInformation)
    {
        CustomerReference = customerInformation.CustomerReference.ToLookup();
    }

    public LookupResponse<Guid>? CustomerReference { get; init; }

    public static implicit operator NusukMasarTicketCustomerInformation(TicketCustomerInformation customerInformation)
        => new(customerInformation);
}
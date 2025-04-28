using Microsoft.Xrm.Sdk;
using MOHU.Integration.Domain.Features.Tickets.Entities;

namespace MOHU.Integration.Contracts.Tickets.Dtos.Responses;

public class NusukMasarTicketCustomerInformation
{
    private NusukMasarTicketCustomerInformation(TicketCustomerInformation customerInformation)
    {
        CustomerReference = customerInformation.CustomerReference;
    }

    public EntityReference? CustomerReference { get; init; }

    public static implicit operator NusukMasarTicketCustomerInformation(TicketCustomerInformation customerInformation)
        => new(customerInformation);
}
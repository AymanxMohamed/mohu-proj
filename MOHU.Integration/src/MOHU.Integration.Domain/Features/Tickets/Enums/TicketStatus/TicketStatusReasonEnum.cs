namespace MOHU.Integration.Domain.Features.Tickets.Enums;

public enum TicketStatusReasonEnum
{
    // Active Statuses == 0
    InProgress = 1,
    OnHold = 2,
    WaitingForDetails = 3,
    Researching = 4,
    
    // Resolved Statuses == 1
    InformationProvided = 1000,
    TicketResolved = 5,
    TicketNotResolved = 749000000,
    
    // Cancelled == 2
    Cancelled = 6,
    Merged = 2000
}
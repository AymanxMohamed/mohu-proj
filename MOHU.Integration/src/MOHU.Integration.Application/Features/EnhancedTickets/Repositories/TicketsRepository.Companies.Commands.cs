using Microsoft.Extensions.Logging;
using MOHU.Integration.Domain.Features.Tickets;
using MOHU.Integration.Domain.Features.Tickets.Entities;

namespace MOHU.Integration.Application.Features.EnhancedTickets.Repositories;

internal partial class TicketsRepository
{
    public Ticket UpdateCompanyTicket(Guid companyId, Guid ticketId, UpdateTicketRequest request)
    {
        var ticket = GetCompanyTicket(companyId, ticketId, columnSet: TicketIntegrationInformation.TicketUpdateColumnSet);
        ticket.EnsureCanUpdateAsCompany(companyId);
        request.Update(ticket);
        genericRepository.Update(ticket.ToCrmEntity());
        
        backgroundTaskQueue.Enqueue(_ =>
        {
            try
            {
                genericRepository.Commit();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during async commit.");
            }
            
            return Task.CompletedTask;
        });
        
        // Task.Run(() =>
        // {
        //     try
        //     {
        //         genericRepository.Commit();
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Error during async commit: {ex.Message}");
        //     }
        // });
        return ticket;
    }
}
using MOHU.Integration.Application.Nusuk.Common.Dtos.Responses;
using MOHU.Integration.Application.Nusuk.Tickets.Dtos.Requests;

namespace MOHU.Integration.Application.Nusuk.Tickets;

public interface INusukTicketsClient
{
    public Task<NusukResponseRoot> UpdateAsync(UpdateNusukTicketRequest request);
}
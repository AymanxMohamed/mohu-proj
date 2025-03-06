using MOHU.Integration.Application.Elm.Nusuk.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.Nusuk.Tickets.Dtos.Requests;

namespace MOHU.Integration.Application.Elm.Nusuk.Tickets;

public interface INusukTicketsClient
{
    public Task<NusukResponseRoot> UpdateAsync(UpdateNusukTicketRequest request);
}
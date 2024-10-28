using MOHU.Integration.Application.Nusuk.Common.Dtos.Responses;
using MOHU.Integration.Application.Nusuk.Tickets.Dtos.Requests;

namespace MOHU.Integration.Application.Nusuk.Tickets;

public interface INusukTicketClient
{
    public Task<NusukRootResponse> UpdateAsync(UpdateNusukTicketRequest request);
}
using MOHU.Integration.Application.Elm.Nusuk.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.Nusuk.Tickets;
using MOHU.Integration.Application.Elm.Nusuk.Tickets.Dtos.Requests;

namespace MOHU.Integration.WebApi.Features.Tickets.Nusuk.Proxies;

[Route("api/proxies/nusuk/tickets")]
[ApiController]
public class NusukTicketsController(INusukTicketsClient nusukTicketsClient) : BaseController
{
    [HttpPut]
    [ProducesResponseType<NusukResponse>(StatusCodes.Status200OK)]
    public async Task<ResponseMessage<NusukResponse>> Update(UpdateNusukTicketRequest request)
    {
        return Ok((await nusukTicketsClient.UpdateAsync(request)).Response);
    }
}
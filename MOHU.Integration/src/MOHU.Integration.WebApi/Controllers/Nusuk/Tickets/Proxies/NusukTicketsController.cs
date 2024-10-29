using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Application.Nusuk.Common.Dtos.Responses;
using MOHU.Integration.Application.Nusuk.Tickets;
using MOHU.Integration.Application.Nusuk.Tickets.Dtos.Requests;
using MOHU.Integration.Contracts.Dto.Common;

namespace MOHU.Integration.WebApi.Controllers.Nusuk.Tickets.Proxies;

[Route("api/proxies/nusuk/tickets")]
[ApiController]
public class NusukTicketsController(INusukTicketsClient nusukTicketsClient) : BaseController
{
    [HttpPut]
    [ProducesResponseType<NusukRootResponse>(StatusCodes.Status200OK)]
    public async Task<ResponseMessage<NusukRootResponse>> Update(UpdateNusukTicketRequest request)
    {
        return Ok(await nusukTicketsClient.UpdateAsync(request));
    }
}
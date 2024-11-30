using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Application.Nusuk.Common.Dtos.Responses;
using MOHU.Integration.Application.Nusuk.Tickets;
using MOHU.Integration.Application.Nusuk.Tickets.Dtos.Requests;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.WebApi.Controllers;

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
﻿using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.Sahab;
using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SahabController(ISahabService sahabService) : BaseController
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseMessage<bool?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage<bool>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    [Route(nameof(UpdateStatus))]
    public async Task<ResponseMessage<bool>> UpdateStatus(SahabUpdateStatusRequest request)
    {
        var result =  await sahabService.UpdateStatusAsync(request);
        return Ok(result);
    }
}
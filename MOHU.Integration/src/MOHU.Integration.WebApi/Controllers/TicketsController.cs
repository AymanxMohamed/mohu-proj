﻿using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.Ticket;
using MOHU.Integration.Contracts.Interface.Ticket;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/{customerId}/[controller]")]
    [ApiController]
    public class TicketsController(ITicketService ticketService) : BaseController
    {
        private readonly ITicketService _ticketService = ticketService;

        [HttpGet("{ticketNumber}")]
        public async Task<ResponseMessage<TicketDetailsResponse>> Get(Guid customerId, string ticketNumber)
        {
            var result = await _ticketService.GetTicketDetailsAsync(customerId, ticketNumber);
            return new ResponseMessage<TicketDetailsResponse> { StatusCode = StatusCodes.Status200OK, Result = result, Status = Contracts.Enum.Status.Success };
        }
        [HttpGet]
        public async Task<ResponseMessage<TicketListResponse>> GetAll(Guid customerId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _ticketService.GetAllTicketsAsync(customerId, pageNumber, pageSize);
            return new ResponseMessage<TicketListResponse> { StatusCode = StatusCodes.Status200OK, Result = result };
        }
        [HttpPost]
        public async Task<ResponseMessage<SubmitTicketResponse>> Post(Guid customerId, [FromBody] SubmitTicketRequest request)
        {
            var result = await _ticketService.SubmitTicketAsync(customerId, request);
            return new ResponseMessage<SubmitTicketResponse> { StatusCode = StatusCodes.Status200OK, Result = result };
        }
    }
}
using MOHU.Integration.Application.Elm.Nusuk.Tickets.Enums;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace MOHU.Integration.Application.Elm.Nusuk.Tickets.Dtos.Requests;

public record UpdateNusukTicketRequest(
    [JsonProperty(nameof(CRMTicketNumber))] string CRMTicketNumber, 
    [JsonProperty(nameof(Comments))] string Comments, 
    [JsonProperty(nameof(Status))] NusukTicketStatusEnum Status);
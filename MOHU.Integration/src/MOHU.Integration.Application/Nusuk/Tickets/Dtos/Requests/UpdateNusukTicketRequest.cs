using MOHU.Integration.Application.Nusuk.Tickets.Enums;

namespace MOHU.Integration.Application.Nusuk.Tickets.Dtos.Requests;

public record UpdateNusukTicketRequest(string CrmTicketNumber, string Comments, NusukTicketStatusEnum Status);
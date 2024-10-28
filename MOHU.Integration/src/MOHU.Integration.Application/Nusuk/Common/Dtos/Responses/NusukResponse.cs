namespace MOHU.Integration.Application.Nusuk.Common.Dtos.Responses;

public record NusukResponse(int ResponseCode, string Message, NusukErrorList ErrorList);
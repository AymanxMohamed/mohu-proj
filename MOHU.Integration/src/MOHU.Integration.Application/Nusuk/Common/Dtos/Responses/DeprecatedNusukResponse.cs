namespace MOHU.Integration.Application.Nusuk.Common.Dtos.Responses;

public record DeprecatedNusukResponse(int ResponseCode, string Message, NusukErrorList ErrorList);
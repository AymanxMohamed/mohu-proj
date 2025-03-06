namespace MOHU.Integration.Application.Elm.Nusuk.Common.Dtos.Responses;

public record DeprecatedNusukResponse(int ResponseCode, string Message, NusukErrorList ErrorList);
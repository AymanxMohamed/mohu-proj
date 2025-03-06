namespace MOHU.Integration.Application.Elm.Nusuk.Common.Dtos.Responses;

public record FaultResponse(int Code, string Type, string Message, string Description)
{
    private string GetFaultMessage() => $"NUSUK Error of type: '{Type}' with message '{Message}' and Description: '{Description}'";
    
    public void Throw()
    {
        throw Code switch
        {
            404 => new NotFoundException(GetFaultMessage()),
            >= 400 => new FluentValidation.ValidationException(GetFaultMessage()),
            _ => new ApplicationException(GetFaultMessage())
        };
    }
}
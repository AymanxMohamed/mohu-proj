namespace Core.Domain.ErrorHandling.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message): base(message)
    {
            
    }
}
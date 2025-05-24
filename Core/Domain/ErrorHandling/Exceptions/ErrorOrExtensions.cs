using Microsoft.AspNetCore.Authentication;

namespace Core.Domain.ErrorHandling.Exceptions;

public static class ErrorOrExtensions
{
    public static T ToValueOrException<T>(this ErrorOr<T> errorOr)
    {
        if (errorOr.IsError)
        {
            errorOr.FirstError.ToException();
        }

        return errorOr.Value;
    }

    private static int ToException(this Error error)
    {
        return error.Type switch
        {
            ErrorType.Validation => throw new BadRequestException(error.Description),
            ErrorType.Conflict => throw new BadRequestException(error.Description),
            ErrorType.NotFound => throw new NotFoundException(error.Description),
            ErrorType.Unauthorized => throw new AuthenticationFailureException(error.Description),
            ErrorType.Forbidden => throw new UnauthorizedAccessException(error.Description),
            ErrorType.Failure => throw new Exception(error.Description),
            ErrorType.Unexpected => throw new Exception(error.Description),
            _ => throw new Exception(error.Description)
        };
    }
}
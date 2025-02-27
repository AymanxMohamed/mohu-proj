using System.Net;

namespace Core.Domain.ErrorHandling.Extensions;

public static class ErrorOrHttpExtensions
{
    public static Error ToErrorOrError(this HttpStatusCode statusCode, string? errorMessage = null)
    {
        return statusCode switch
        {
            HttpStatusCode.Conflict or HttpStatusCode.BadRequest => errorMessage is not null ? Error.Validation(description: errorMessage) : Error.Validation(),
            HttpStatusCode.NotFound => errorMessage is not null ? Error.NotFound(description: errorMessage) : Error.NotFound(),
            HttpStatusCode.Unauthorized => errorMessage is not null ? Error.Unauthorized(description: errorMessage) : Error.Unauthorized(),
            HttpStatusCode.Forbidden => errorMessage is not null ? Error.Forbidden(description: errorMessage) : Error.Forbidden(),
            _ => errorMessage is not null ? Error.Failure(description: errorMessage) : Error.Failure(),
        };
    }
    
    public static ErrorOr<T> EnsureNotNull<T>(this T? type)
    {
        if (type is null)
        {
            return Error.Validation($"{typeof(T).Name} doesn't exist");
        }

        return type;
    }


}
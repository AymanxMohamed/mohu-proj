using System.Net;
using MOHU.Integration.Contracts.Logging;

namespace MOHU.Integration.WebApi.Common.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAppLogger _logger;
        public GlobalExceptionHandlerMiddleware(RequestDelegate next , IAppLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await _logger.LogError(ex);
                await HandleExceptionAsync(context, ex);
            }
        }


        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if(ex is BadRequestException)
                 HandleBadRequestException(context, ex);
            else if(ex is NotFoundException)
                HandleNotFoundException(context, ex);
            else if(ex is ValidationException)
                HandleValidationException(context, ex);             
            else
                HandleUnknownException(context, ex);

            return Task.CompletedTask;
        }

        private Task HandleValidationException(HttpContext context, Exception ex)
        {
            var exception = ex as ValidationException;

            var details = new ResponseMessage<object>
            {
                Status = Contracts.Enum.Status.Failure,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Result = null,
                ErrorMessage = exception?.Errors.FirstOrDefault().Value.FirstOrDefault()
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(details);
        }

        private Task HandleBadRequestException(HttpContext context, Exception ex)
        {
            var exception = ex as BadRequestException;

            var details = new ResponseMessage<object>
            {
                Status = Contracts.Enum.Status.Failure,
                StatusCode =StatusCodes.Status400BadRequest,
                Result = null,
                ErrorMessage = exception?.Message
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(details);
        }
        private Task HandleNotFoundException(HttpContext context, Exception ex)
        {
            var exception = ex as NotFoundException;

            var details = new ResponseMessage<object>
            {
                Status = Contracts.Enum.Status.Failure,
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = exception?.Message,
                Result = null
            };
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(details);
        }

        private Task HandleUnknownException(HttpContext context, Exception ex)
        {
            var details = new ResponseMessage<object>
            {
                Status = Contracts.Enum.Status.Failure,
                StatusCode = StatusCodes.Status500InternalServerError,
                 ErrorMessage= ex.Message,
                Result = null
            };
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(details);

        }
    }
}

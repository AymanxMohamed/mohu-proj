using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using MOHU.Integration.Application.Exceptions;

namespace MOHU.Integration.Application.Common.Extensions;

public static class ValidationResultExtensions
{
    public static bool IsValid(this ValidationResult? validationResult) => validationResult is null or { IsValid: true };
    
    public static ValidationFailure? ToValidationFailure(this ValidationResult? validationResult)
    {
        return validationResult.IsValid() ? null : validationResult?.Errors?.FirstOrDefault();
    }
    
    public static void EnsureValidResult(
        this ValidationResult? validationResult, 
        IStringLocalizer? stringLocalizer = null)
    {
        if (validationResult.IsValid())
        {
            return;
        }

        var validationFailure = validationResult.ToValidationFailure();

        if (validationFailure == null)
        {
            throw new BadRequestException("Validation failed");
        };
        
        var errorMessage = stringLocalizer != null 
            ? stringLocalizer[validationFailure.ErrorMessage] 
            : validationFailure.ErrorMessage;

        throw new BadRequestException($"Validation failed: {errorMessage}. Property: {validationFailure.PropertyName}");
    }
}
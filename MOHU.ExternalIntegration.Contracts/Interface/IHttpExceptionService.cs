using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace MOHU.ExternalIntegration.Contracts.Interface
{
    public interface IHttpExceptionService
    {
        void ThrowBadRequestError(string message);
        void ThrowNotFoundError(string message);
        bool ValidateModel(object model, out List<string> errorMessages);

        Task<FluentValidation.Results.ValidationResult> ValidateModelAsync<TModel, TValidator>(TModel model) where TValidator : IValidator<TModel>, new();

    }
}

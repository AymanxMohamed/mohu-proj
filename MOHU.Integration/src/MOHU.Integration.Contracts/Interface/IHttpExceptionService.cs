

using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IHttpExceptionService
    {
        void ThrowBadRequestError(string message);
        void ThrowNotFoundError(string message);
        bool ValidateModel(object model, out List<string> errorMessages);

        //   Task<ValidationResult> ValidateModelAsync<TModel, TValidator>(TModel model) where TValidator : IValidator<TModel>, new();
        Task<FluentValidation.Results.ValidationResult> ValidateModelAsync<TModel, TValidator>(TModel model, TValidator validator)
       where TValidator : IValidator<TModel>;

    }
}

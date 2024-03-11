
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;


namespace MOHU.Integration.Application.common
{
    public class HttpExceptionService: IHttpExceptionService
    {
        public void ThrowBadRequestError(string message)
        {
            throw new BadRequestException(message);
        }
        public void ThrowNotFoundError(string message)
        {
            throw new NotFoundException(message);
        }

        //public async Task<FluentValidation.Results.ValidationResult> ValidateModelAsync<TModel, TValidator>(TModel model) where TValidator : FluentValidation.IValidator<TModel>, new()
        //{
        //    if (model == null)
        //    {

        //        return new FluentValidation.Results.ValidationResult { Errors = { new ValidationFailure("Model", "model cannot be null.") } };

        //    }
        //    var validator = new TValidator();
        //    var results = await validator.ValidateAsync(model);

        //    return await Task.FromResult(results);
        //}

        // new method 
        public async Task<FluentValidation.Results.ValidationResult> ValidateModelAsync<TModel, TValidator>(TModel model, TValidator validator)
                   where TValidator : IValidator<TModel>
        {
            if (model == null)
            {
                return new FluentValidation.Results.ValidationResult { Errors = { new ValidationFailure("Model", "model cannot be null.") } };
            }

            var results = await validator.ValidateAsync(model);
            return results;
        }

        public bool ValidateModel(object model, out List<string> errorMessages)
        {
            var context = new ValidationContext(model);
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            bool isValid = Validator.TryValidateObject(model, context, validationResults, true);

            if (!isValid)
            {
                errorMessages = new List<string>();
                errorMessages.AddRange(validationResults.Select(e => e.ErrorMessage));
                return false;
            }

            errorMessages = null;
            return true;
        }



    }
}

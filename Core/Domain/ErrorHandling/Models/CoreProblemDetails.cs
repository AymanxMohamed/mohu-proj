using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Core.Domain.ErrorHandling.Models;

public class CoreProblemDetails : ProblemDetails
{
    public Dictionary<string, List<string>>? Errors { get; set; }

    public ModelStateDictionary? ModelState
    {
        get
        {
            if (Errors is null)
            {
                return null;
            }
            
            var modelStateDictionary = new ModelStateDictionary();

            foreach (string key in Errors.Keys)
            {
                var firstError = Errors[key].FirstOrDefault();

                if (firstError is not null)
                {
                    modelStateDictionary.AddModelError(key, firstError);
                }
            }

            return modelStateDictionary;
        }
    }
}
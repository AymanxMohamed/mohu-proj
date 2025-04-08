using MOHU.Integration.Contracts.Dto.CreateProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Service
{
    public class CategorieServices
    {
        private readonly ICrmContext _crmContext;

        private readonly IStringLocalizer _localizer;
        private readonly IValidator<CreateProfileRequest> _validator;

        public CategorieServices(ICrmContext crmContext,
            IStringLocalizer localizer,
            IValidator<CreateProfileRequest> validator)
        {
            _crmContext = crmContext;
            _localizer = localizer;
            _validator = validator;


        }

        public async Task<Guid> UpsertCategories(CreateProfileRequest model)
        {
            var results = await _validator.ValidateAsync(model);

            if (results?.IsValid == false)
            {
                throw new BadRequestException(results.Errors?.FirstOrDefault()?.ErrorMessage ?? string.Empty);
            }

            var entity = new Entity(TicketCategory.EntityName);

        }

        private async Task<Guid?> IsCategoryExists(string TicketType, string CategoryId , int statecode)
        {
            // var query_statecode = 0;
            var CategoryQuery = new QueryExpression
            {
                EntityName = TicketCategory.EntityName,
                NoLock = true
            };
            CategoryQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, statecode);
            CategoryQuery.Criteria.AddCondition("ldv_tickettypeid", ConditionOperator.Equal, TicketType);
            CategoryQuery.Criteria.AddCondition("ldv_casecategoryid", ConditionOperator.Equal, CategoryId);

            var response = await _crmContext.ServiceClient.RetrieveMultipleAsync(CategoryQuery);
            return response.Entities.Count > 0 ? response?.Entities?.FirstOrDefault()?.Id : null;
        }

    }
}

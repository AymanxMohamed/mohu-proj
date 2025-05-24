using Common.Crm.Domain.Common.Extensions;
using MOHU.Integration.Contracts.Dto.Category;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.ErrorHandling.Exceptions;

namespace MOHU.Integration.Application.Service
{
    public class CategorieServices : ITicketCategoryService
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

        

        public async Task<Guid> UpsertCategories(UpsertCategoryRequest model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrEmpty(model.CategoryId))
            {
                // Create new category - validate required fields
                if (string.IsNullOrEmpty(model.ArabicName))
                    throw new ArgumentException("ArabicName is required");
                if (string.IsNullOrEmpty(model.EnglishName))
                    throw new ArgumentException("EnglishName is required");
                if (model.TicketType == null)
                    throw new ArgumentException("TicketType is required");

                var entity = new Entity(TicketCategory.EntityName)
                {
                    Attributes =
            {
                { TicketCategory.Fields.ArabicName, model.ArabicName },
                { TicketCategory.Fields.EnglishName, model.EnglishName },
                { TicketCategory.Fields.TicketType, new EntityReference("ldv_service",new Guid(model.TicketType)) } 
            }
                };

                // Handle Status (OptionSet)
                if (model.Status.HasValue)
                {
                    entity.Attributes.Add(TicketCategory.Fields.Status, new OptionSetValue((int)model.Status.Value));
                }


                // Handle optional fields
                if (model.ParentCategory != null)
                    entity.Attributes.Add(TicketCategory.Fields.ParentCategory, new EntityReference(TicketCategory.EntityName, new Guid(model.ParentCategory)));

                if (model.SubCategory != null)
                    entity.Attributes.Add(TicketCategory.Fields.SubCategory, new EntityReference(TicketCategory.EntityName, new Guid(model.SubCategory)));

                if (model.Priority != null)
                    entity.Attributes.Add(TicketCategory.Fields.Priority,new OptionSetValue((int)model.Priority.Value));

                if (model.ComplainType != null)
                    entity.Attributes.Add(TicketCategory.Fields.ComplainType, new OptionSetValue((int)model.ComplainType.Value));

                if (model.Season != null)
                    entity.Attributes.Add(TicketCategory.Fields.Season, new OptionSetValue((int)model.Season.Value));

               

                return await _crmContext.ServiceClient.CreateAsync(entity);
            }
            else
            {
                // Update existing category
                var existingCategory = await IsCategoryExists(model.TicketType, model.CategoryId);
                if (existingCategory == null)
                    throw new NotFoundException("Category not found");

                var categoryToUpdate = new Entity(TicketCategory.EntityName, Guid.Parse(model.CategoryId));

                // Update only non-null fields
                if (model.ArabicName != null)
                    categoryToUpdate.Attributes.Add(TicketCategory.Fields.ArabicName, model.ArabicName);

                if (model.EnglishName != null)
                    categoryToUpdate.Attributes.Add(TicketCategory.Fields.EnglishName, model.EnglishName);

                if (model.ParentCategory != null)
                    categoryToUpdate.Attributes.Add(TicketCategory.Fields.ParentCategory,
                        new EntityReference(TicketCategory.EntityName, new Guid(model.ParentCategory)));

                if (model.SubCategory != null)
                    categoryToUpdate.Attributes.Add(TicketCategory.Fields.SubCategory,
                        new EntityReference(TicketCategory.EntityName, new Guid(model.SubCategory)));

                // Handle Status (OptionSet)
                if (model.Status.HasValue)
                {
                    categoryToUpdate.Attributes.Add(TicketCategory.Fields.Status, new OptionSetValue((int)model.Status.Value));
                }

                if (model.TicketType != null)
                    categoryToUpdate.Attributes.Add(TicketCategory.Fields.TicketType,
                        new EntityReference("ldv_service", new Guid(model.TicketType)));

                if (model.Priority != null)
                    categoryToUpdate.Attributes.Add(TicketCategory.Fields.Priority, new OptionSetValue((int)model.Priority.Value));

                if (model.ComplainType != null)
                    categoryToUpdate.Attributes.Add(TicketCategory.Fields.ComplainType,  new OptionSetValue((int)model.ComplainType.Value));

                if (model.Season != null)
                    categoryToUpdate.Attributes.Add(TicketCategory.Fields.Season, new OptionSetValue((int)model.Season.Value));

               

                if (categoryToUpdate.Attributes.Any())
                {
                    await _crmContext.ServiceClient.UpdateAsync(categoryToUpdate);
                }

                return Guid.Parse(model.CategoryId);
            }
        }
        private async Task<Guid?> IsCategoryExists(string TicketType, string CategoryId )
        {
            // var query_statecode = 0;
            var CategoryQuery = new QueryExpression
            {
                EntityName = TicketCategory.EntityName,
                NoLock = true
            };
            CategoryQuery.Criteria.AddCondition("ldv_tickettypeid", ConditionOperator.Equal, TicketType);
            CategoryQuery.Criteria.AddCondition("ldv_casecategoryid", ConditionOperator.Equal, CategoryId);



            var response = await _crmContext.ServiceClient.RetrieveMultipleAsync(CategoryQuery);
            return response.Entities.Count > 0 ? response?.Entities?.FirstOrDefault()?.Id : null;
        }

    }
}

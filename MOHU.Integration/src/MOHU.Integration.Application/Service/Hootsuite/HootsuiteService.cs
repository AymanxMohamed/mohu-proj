using Core.Domain.ErrorHandling.Exceptions;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Http.HttpResults;
using MOHU.Integration.Application.Features.Tickets.Services;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Dto.Hootsuite;
using MOHU.Integration.Contracts.Interface.Customer;
using MOHU.Integration.Contracts.Tickets.Dtos.Requests;


namespace MOHU.Integration.Application.Service.Hootsuite;
internal class HootsuiteService(ICustomerService customerService,ITicketService ticketService) : IHootsuiteService
{
    public async Task<string> ConversationResolved(ConversationResolvedRequest conversationResolvedRequest)
    {
        Guid? customerId =  await customerService.FindOrCreateProfileAsync(conversationResolvedRequest);
        var categoryId = conversationResolvedRequest.Categories.FirstOrDefault()?.Id;
        Guid? requestType = null;
        List<TicketCategoryLevel> categoriesWithLevels = [];
        if (categoryId != null)
        {
             requestType = await ticketService.GetCategoryRequestType(categoryId.GetValueOrDefault());
             categoriesWithLevels = await ticketService.GetCategoriesLevel(conversationResolvedRequest.Categories.Select(cat => cat.Id).ToList());
            try
            {

            if (!categoriesWithLevels.Any(c => c.CategoryLevel == Contracts.Enum.CategoryLevelsEnum.SubCategory) && categoriesWithLevels.Any(c => c.CategoryLevel == Contracts.Enum.CategoryLevelsEnum.SecondryCategory))
            {
                var secondryCategory = categoriesWithLevels.FirstOrDefault(c => c.CategoryLevel == Contracts.Enum.CategoryLevelsEnum.SecondryCategory);
                var subCategoryId = await ticketService.GetSubCategory(secondryCategory!.Id);
                categoriesWithLevels.Add(new TicketCategoryLevel { Id = subCategoryId, CategoryLevel = Contracts.Enum.CategoryLevelsEnum.SubCategory , ParentId = secondryCategory.ParentId});
            }
            if (!categoriesWithLevels.Any(c => c.CategoryLevel == Contracts.Enum.CategoryLevelsEnum.ParentCategory) && categoriesWithLevels.Any(c => c.CategoryLevel == Contracts.Enum.CategoryLevelsEnum.SubCategory))
            {
                var subCategory = categoriesWithLevels.FirstOrDefault(c => c.CategoryLevel != Contracts.Enum.CategoryLevelsEnum.ParentCategory);
                var parentCategoryId = await ticketService.GetParentCategory(subCategory!.ParentId);
                categoriesWithLevels.Add(new TicketCategoryLevel { Id = parentCategoryId , CategoryLevel = Contracts.Enum.CategoryLevelsEnum.ParentCategory });
            }
            }catch(Exception ex)
            {
                throw new NotFoundException("Something went wrong while fetching parent categories");
            }
            

        }
        string description = string.Join(", ", conversationResolvedRequest.Notes.Select(n => n.Text));
        CreateHootsuiteTicketWithCategoryRequest newCase = new CreateHootsuiteTicketWithCategoryRequest
        {
            CaseType = requestType.GetValueOrDefault(),
            Description = description,
            Categories = categoriesWithLevels
        };

        var ticket = await ticketService.SubmitHootSuiteTicketWithCategoryAsync(customerId.GetValueOrDefault(), newCase);
        return ticket.TicketNumber;
    }
};
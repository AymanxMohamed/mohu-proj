using Core.Domain.ErrorHandling.Exceptions;
using Microsoft.AspNetCore.Http;

namespace MOHU.Integration.Domain.Features.Tickets;

public partial class Ticket
{
    private const string CompaniesStatusName = "قيد العمل من قبل شركات مقدمي الخدمة";
    private static readonly Guid CompaniesStatusId = new("b336c695-3fcb-ee11-907a-6045bd8c92a2");
    
    public void EnsureCanUpdateAsCompany(Guid companyId)
    {
        if (BasicInformation.Company?.Id != companyId)
        {
            throw new BadRequestException($"Company with this Id: {companyId} can't access this ticket.");
        }
        
        if (!IsEligibleForUpdate())
        {
            throw new BadRequestException($"Ticket: {BasicInformation.Title} Already updated.");
        }

        if (BasicInformation.StatusReason?.Id != CompaniesStatusId &&
            BasicInformation.StatusReason?.Name != CompaniesStatusName)
        {
            throw new BadRequestException($"Ticket can only be updated if the status is: {CompaniesStatusName}");
        }
    }
    
    
    private bool IsEligibleForUpdate()
    {
        return IsEligibleForUpdateNusukEnaya() 
               || IsEligibleForUpdateNusukTheRest();
    }

    private bool IsEligibleForUpdateNusukEnaya()
    {
        return Classification.IsNusukEnayaServices() 
               && (
                   IntegrationInformation.IsNusukPortalUpdated is null 
                   || !IntegrationInformation.IsNusukPortalUpdated.Value
                   || (IntegrationInformation.CompanyServiceDecisionCode is null 
                       && IntegrationInformation.IsNusukPortalUpdated is not null 
                       && IntegrationInformation.IsNusukPortalUpdated.Value));
    }
    
    private bool IsEligibleForUpdateNusukTheRest()
    {
        return IntegrationInformation.CompanyPortalUpdated is null 
               || !IntegrationInformation.CompanyPortalUpdated.Value
               || (IntegrationInformation.DepartmentDecision is null 
                   && IntegrationInformation.CompanyPortalUpdated is not null 
                   && IntegrationInformation.CompanyPortalUpdated.Value);
    }
}

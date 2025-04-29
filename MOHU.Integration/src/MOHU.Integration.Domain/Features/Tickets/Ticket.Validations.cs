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
            throw new InvalidOperationException($"Company with this Id: {companyId} can't access this ticket.");
        }

        if (BasicInformation.StatusReason?.Id != CompaniesStatusId &&
            BasicInformation.StatusReason?.Name != CompaniesStatusName)
        {
            throw new InvalidOperationException($"Company with this Id: {companyId} can't access this ticket.");
        }
    }
}

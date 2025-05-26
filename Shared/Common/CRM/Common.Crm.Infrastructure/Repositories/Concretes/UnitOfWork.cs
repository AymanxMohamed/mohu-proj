using Microsoft.Xrm.Sdk.Messages;

namespace Common.Crm.Infrastructure.Repositories.Concretes;

public static class UnitOfWork
{
    static UnitOfWork()
    {
        ResetUnitOfWork();
    }
        
    public static OrganizationRequestCollection Requests { get; private set; }
    
    private static ExecuteTransactionRequest ExecuteTransactionRequest { get; set; }

    public static ExecuteTransactionResponse ExecuteRequests(IOrganizationService organizationService)
    {
        try
        {
            var response = (ExecuteTransactionResponse) organizationService.Execute(ExecuteTransactionRequest);
            return response;
        }
        finally
        {
            ResetUnitOfWork();
        }
    }
        
    private static void ResetUnitOfWork()
    {
        Requests = RequestsFactory.CreateCreateRequestCollection();
        ExecuteTransactionRequest = RequestsFactory.CreateExecuteTransactionRequest(Requests);
    }
}
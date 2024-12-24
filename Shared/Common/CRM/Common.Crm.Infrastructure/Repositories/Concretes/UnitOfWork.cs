using Microsoft.Xrm.Sdk.Messages;

namespace Common.Crm.Infrastructure.Repositories.Concretes
{
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
            var response = (ExecuteTransactionResponse) organizationService.Execute(ExecuteTransactionRequest);
            ResetUnitOfWork();
            return response;
        }
        
        private static void ResetUnitOfWork()
        {
            Requests = RequestsFactory.CreateCreateRequestCollection();
            ExecuteTransactionRequest = RequestsFactory.CreateExecuteTransactionRequest(Requests);
        }
    }
}
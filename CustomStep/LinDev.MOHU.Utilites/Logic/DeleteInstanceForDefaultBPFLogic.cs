using LinkDev.MAAN.Common;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinDev.MOHU.Utilites.Logic
{
    public class DeleteInstanceForDefaultBPFLogic : StepLogic<LinDev.MOHU.Utilites.DeleteInstanceForDefaultBPF>
    {
        protected override void ExecuteLogic()
        {

            try
            {
                IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
                IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
                ITracingService tracingService = executionContext.GetExtension<ITracingService>();

                // Get input parameters
                string entityLogicalName = codeActivity.EntityLogicalName.Get(executionContext);
                string entityIdStr = codeActivity.EntityId.Get(executionContext);

                if (!string.IsNullOrEmpty(entityLogicalName) && !string.IsNullOrEmpty(entityIdStr))
                {
                    // Parse the entity ID
                    //if (Guid.TryParse(entityIdStr, out Guid entityId))
                    //{
                        DeleteDefaultBPFInstance(entityLogicalName,  new Guid (entityIdStr));
                    //}
                    //else
                    //{
                    //    throw new Exception("Invalid entity ID format.");
                    //}
                }
                else
                {
                    //throw new Exception("Entity logical name or ID is null or empty.");
                    tracingService.Trace($"Entity logical name or ID is null or empty.");

                }
            }
            catch (Exception ex)
            {
                //throw new InvalidPluginExecutionException($"An error occurred: {ex.Message}");
                tracingService.Trace($"An error occurred: {ex.Message}");

            }
        }

        private void DeleteDefaultBPFInstance(string entityLogicalName, Guid entityId)
        {
            try
            {
                // Delete the default BPF instance
                //service.Delete("processinstance", entityId);
                //service.Delete("phonetocaseprocess", entityId);
                service.Delete(entityLogicalName, entityId);
                tracingService.Trace($"The Request {entityLogicalName} with id {entityId} is deleted.");



            }
            catch (Exception ex)
            {
                //throw new Exception($"Error deleting default BPF instance: {ex.Message}");
                tracingService.Trace($"Error deleting default BPF instance: {ex.Message}");

            }
        }
    }
}

using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Logger;
using LinkDev.Common.Crm.Utilities;
using LinkDev.CRM.Library.DAL;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace LinkDev.Common.Crm.Cs.StageConfiguration
{
    public class AssignTheRequest : CodeActivity
    {
        #region Input Parameter
        [RequiredArgument]
        [Input("RequestId")]

        public InArgument<string> RequestId { get; set; }

        [RequiredArgument]
        [Input("RequestSchemaName")]
        public InArgument<string> RequestSchemaName { get; set; }

        [Input("Assign to User")]
        [Default("False")]
        public InArgument<bool> AssignToUser { get; set; }

        [Input("Assign to Team")]
        [Default("False")]
        public InArgument<bool> AssignToTeam { get; set; }

        [Input("Assign to Queue")]
        [Default("False")]
        public InArgument<bool> AssignToQueue { get; set; }

        [Input("User")]
        [ReferenceTarget("systemuser")]
        public InArgument<EntityReference> User { get; set; }

        [Input("Queue")]
        [ReferenceTarget("queue")]
        public InArgument<EntityReference> Queue { get; set; }

        [Input("Team")]
        [ReferenceTarget("team")]
        public InArgument<EntityReference> Team { get; set; }
        #endregion

        protected override void Execute(CodeActivityContext context)
        {
            AssignTheRequestLogic BL = new AssignTheRequestLogic(RequestId,
                                      RequestSchemaName,
                                      AssignToUser,
                                     AssignToTeam,
                                       AssignToQueue,
                                       User,
                                     Queue,
                                       Team);
            BL.ExecuteLogic(context);
        }
    }





    public class AssignTheRequestLogic
    {
        public InArgument<string> RequestId { get; set; }
        public InArgument<string> RequestSchemaName { get; set; }
        public InArgument<bool> AssignToUser { get; set; }
        public InArgument<bool> AssignToTeam { get; set; }
        public InArgument<bool> AssignToQueue { get; set; }
        public InArgument<EntityReference> User { get; set; }
        public InArgument<EntityReference> Queue { get; set; }
        public InArgument<EntityReference> Team { get; set; }
        protected CRMAccessLayer DAL;

        public AssignTheRequestLogic(InArgument<string> requestId,
                                     InArgument<string> requestSchemaName,
                                     InArgument<bool> assignToUser,
                                     InArgument<bool> assignToTeam,
                                       InArgument<bool> assignToQueue,
                                     InArgument<EntityReference> user,
                                     InArgument<EntityReference> queue,
                                     InArgument<EntityReference> team)
        {

            RequestSchemaName = requestSchemaName;
            RequestId = requestId;
            AssignToUser = assignToUser;
            AssignToTeam = assignToTeam;
            AssignToQueue = assignToQueue;
            User = user;
            Queue = queue;
            Team = team;


        }



        public void ExecuteLogic(CodeActivityContext executionContext)
        {
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            DAL = new CRMAccessLayer(service);

            try
            {
                string requestId = RequestId.Get(executionContext);
                string requestSchemaName = RequestSchemaName.Get(executionContext);
                bool assignToUser = AssignToUser.Get(executionContext);


                #region Assign to user
                if (AssignToUser.Get<bool>(executionContext) && User.Get<EntityReference>(executionContext) == null)
                    throw new Exception($"User is null while you choose Assign to User");


                if (AssignToUser.Get<bool>(executionContext) && User.Get<EntityReference>(executionContext) != null)
                    AssignRecord(new EntityReference(RequestSchemaName.Get<string>(executionContext), new Guid(RequestId.Get<string>(executionContext)))
                    , User.Get<EntityReference>(executionContext));

                #endregion

                #region Assign to Team
                if (AssignToTeam.Get<bool>(executionContext) && Team.Get<EntityReference>(executionContext) == null)
                    throw new Exception($"team is null while you choose Assign to Team");

                if (AssignToTeam.Get<bool>(executionContext) && Team.Get<EntityReference>(executionContext) != null)
                    AssignRequestToTeam(new EntityReference(RequestSchemaName.Get<string>(executionContext),
                                new Guid(RequestId.Get<string>(executionContext))), Team.Get<EntityReference>(executionContext));

                #endregion

                #region Assign to Queue
                if (AssignToQueue.Get<bool>(executionContext) && Queue.Get<EntityReference>(executionContext) == null)
                    throw new Exception($"Queue is null while you choose Assign to Team");

                if (AssignToQueue.Get<bool>(executionContext) && Queue.Get<EntityReference>(executionContext) != null)
                    AssignRequestToQueue(new EntityReference(RequestSchemaName.Get<string>(executionContext),
                                new Guid(RequestId.Get<string>(executionContext))), Team.Get<EntityReference>(executionContext));

                #endregion

            }
            catch (Exception e)
            {

                throw e;
            }

        }
        private void AssignRecord(EntityReference targetEntity, EntityReference targetAssigningOwning)
        {


            if (targetAssigningOwning?.Id != null && targetAssigningOwning?.Id != Guid.Empty &&
                targetEntity?.Id != null && targetEntity?.Id != Guid.Empty)
            {
                Entity request = new Entity(targetEntity.LogicalName, targetEntity.Id);
                request.Attributes.Add("ownerid", targetAssigningOwning);
                DAL.UpdateEntity(request);

            }


        }


        private void AssignRequestToQueue(EntityReference targetEntity, EntityReference targetAssigningOwning)
        {

            Entity queueItem = new Entity("queueitem");
            queueItem.Attributes.Add("queueid", targetAssigningOwning);
            queueItem.Attributes.Add("regardingid", targetEntity);

            DAL.CreateEntity(queueItem);

        }
        private void AssignRequestToTeam(EntityReference targetEntity, EntityReference targetAssigningOwning)
        {

            // Create the Request Object and Set the Request Object's Properties
            if (targetAssigningOwning?.Id != null && targetAssigningOwning?.Id != Guid.Empty &&
             targetEntity?.Id != null && targetEntity?.Id != Guid.Empty)
            {
                Entity request = new Entity(targetEntity.LogicalName, targetEntity.Id);
                request.Attributes.Add("ownerid", targetAssigningOwning);
                DAL.UpdateEntity(request);

            }
        }
        //Assign a record to a team/user


    }
}

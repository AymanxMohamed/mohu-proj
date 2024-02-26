//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Microsoft.Xrm.Sdk;
//using Moq;
//using System.Activities;
//using Microsoft.Xrm.Sdk.Workflow;
//using Microsoft.Xrm.Client;
//using Microsoft.Xrm.Client.Services;

//using System.ServiceModel.Description;
//using Microsoft.Xrm.Sdk.Client;
 

//namespace Linkdev.Common.Moq
//{
//    class plugin
//    {
//        static void Main(string[] args)
//        {

//            #region Variables Intializations

//            //sms
//            //Guid AdminUserId = new Guid("0460D6D5-0B94-E811-80CE-000D3A393005");
//            //Guid PrimaryEntityID = new Guid("ED11608A-9419-E911-8124-000D3A393005");
//            //string PrimaryEntityName = "ldv_sms";

//            //portal
//            Guid AdminUserId = new Guid("c7b3288f-8610-ec11-a957-6045bd8810e2");
//            Guid PrimaryEntityID = new Guid("7ad6eb6a-5d78-ec11-aa15-6045bd8810e2");
//            string PrimaryEntityName = "ldv_casecategory";

//            //set up mock plugincontext with input/output parameters, etc.
//            Entity targetEntity = new Entity(PrimaryEntityName);


//            //targetEntity.Attributes["description"] = "";
//            //targetEntity.Attributes["ldv_mobilenumber"] = "01009612167"; 
//            //targetEntity.Attributes["description"] = new EntityReference("ldv_servicestatus", Guid.Parse("3CDC0E8D-0A31-E811-8254-000D3A28225B"));
//            targetEntity.LogicalName = PrimaryEntityName;

//            //Entity PreImageEntity = new Entity(PrimaryEntityName);
//            //PreImageEntity.Attributes["ldv_servicestatus"] = new EntityReference("ldv_servicestatus", Guid.Parse("0C66A0D1-D72D-E811-824F-000D3A28225B"));
//            //PreImageEntity.LogicalName = PrimaryEntityName;


//            #endregion

//            //Activity SendPeerReviewersLink = new SendPeerReviewersLink();
//            //var invoker = new WorkflowInvoker(SendPeerReviewersLink);

//            //Activity CreateComplaintRelatedDecision = new CreateComplaintRelatedDecision();
//            //var invoker = new WorkflowInvoker(CreateComplaintRelatedDecision);

//            //Activity DeletePeerReviewTechnicalSummary = new DeletePeerReviewTechnicalSummary();
//            //var invoker = new WorkflowInvoker(DeletePeerReviewTechnicalSummary);
//            //IPlugin pluginCreateCPUIssue = new CreateCPUIssue();
//            //var invoker = new WorkflowInvoker(pluginCreateCPUIssue);

//            // Activity SendEmailToFTPPMembers = new SendEmailToFTPPMembers();
//            // var invoker = new WorkflowInvoker(SendEmailToFTPPMembers);

//            //Activity FTPPComplaintRelatedDecision = new FTPPComplaintRelatedDecision();
//            //var invoker = new WorkflowInvoker(FTPPComplaintRelatedDecision);

//            // Activity Notify = new SendNotification();
//            // var invoker = new WorkflowInvoker(Notify);

//            //create our mocks
//            var factoryMock = new Mock<IOrganizationServiceFactory>();
//            var tracingServiceMock = new Mock<ITracingService>();
//            var workflowContextMock = new Mock<IWorkflowContext>();
//            var serviceMock = new Mock<IOrganizationService>();
//            var notificationServiceMock = new Mock<IServiceEndpointNotificationService>();
//            var pluginContextMock = new Mock<IPluginExecutionContext>();
//            var serviceProviderMock = new Mock<IServiceProvider>();

//            //set up a mock service for CRM organization service


//            //next - create an entity object that will allow us to capture the entity record that is passed to the Create method
//            Entity actualEntity = new Entity();
//            Guid idToReturn = Guid.NewGuid();
//            //setup the CRM service mock
//            serviceMock.Setup(t =>
//                t.Update(It.IsAny<Entity>()))
//                //when Create is called with any entity as an invocation parameter
//                //.Returns(idToReturn) //return the idToReturn guid
//                .Callback<Entity>(s => targetEntity = s); //store the Create method invocation parameter for inspection later

//            //IOrganizationService service  = XrmConnectionProvider.Service;
//            //IOrganizationService service = serviceMock.Object;
//            //var connection = CrmConnection.Parse(@"Url=https://moe.linkdev.com/XRMServices/2011/Organization.svc; Username=crm20160\crmadmin; Password=P@ssw0rd@MOE;");


//            ////var connection2 = CrmConnection.Parse(@"AuthType=IFD;Url=https://moe.linkdev.com; HomeRealmUri=https://moe.linkdev.com/MOE/XRMServices/2011/Organization.svc;Domain=crm20160; Username=crmadmin; Password=P@ssw0rd@MOE");
//            //OrganizationService service = new OrganizationService(connection);

//            IOrganizationService service = GetCrmConnection("https://stc-dev-v9.linkdev.com/XRMServices/2011/Organization.svc", @"CRM365DC\crmadmin", "linkP@ss");

//            //set up a mock servicefactory using the CRM service mock
//            factoryMock.Setup(t => t.CreateOrganizationService(It.IsAny<Guid>())).Returns(service);
//            var factory = factoryMock.Object;

//            //set up a mock tracingservice - will write output to console
//            tracingServiceMock.Setup(t => t.Trace(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((t1, t2) => Console.WriteLine(t1, t2));
//            var tracingService = tracingServiceMock.Object;

//            //set up mock notificationservice - not going to do anything with this
//            var notificationService = notificationServiceMock.Object;



//            //ACT
//            //instantiate the plugin object and execute it with the testserviceprovider






//            //IOrganizationService service = new CRMAccess().GetAccessToCRM();

//            //workflowContextMock.Setup(t => t.InitiatingUserId).Returns(AdminUserId);
//            //workflowContextMock.Setup(t => t.CorrelationId).Returns(Guid.NewGuid());
//            //workflowContextMock.Setup(t => t.UserId).Returns(AdminUserId);
//            //workflowContextMock.Setup(t => t.PrimaryEntityId).Returns(PrimaryEntityID);
//            //workflowContextMock.Setup(t => t.PrimaryEntityName).Returns(PrimaryEntityName);
//            //var workflowContext = workflowContextMock.Object;


//            ParameterCollection inputParameters = new ParameterCollection();
//            inputParameters.Add("Target", targetEntity);
//            //EntityImageCollection imageCollection = new EntityImageCollection();
//            //imageCollection.Add("PreImage", PreImageEntity);
//            ParameterCollection outputParameters = new ParameterCollection();
//            outputParameters.Add("id", PrimaryEntityID);

//            pluginContextMock.Setup(t => t.InputParameters).Returns(inputParameters);
//            pluginContextMock.Setup(t => t.OutputParameters).Returns(outputParameters);
//            //pluginContextMock.Setup(t => t.PreEntityImages).Returns(imageCollection);
//            //pluginContextMock.Setup(t => t.PostEntityImages).Returns(imageCollection);
//            pluginContextMock.Setup(t => t.UserId).Returns(AdminUserId);
//            pluginContextMock.Setup(t => t.PrimaryEntityName).Returns(PrimaryEntityName);
//            pluginContextMock.Setup(t => t.PrimaryEntityId).Returns(PrimaryEntityID);
//            pluginContextMock.Setup(t => t.MessageName).Returns("create");
//            var pluginContext = pluginContextMock.Object;

//            //set up a serviceprovidermock
//            serviceProviderMock.Setup(t => t.GetService(It.Is<Type>(i => i == typeof(IServiceEndpointNotificationService)))).Returns(notificationService);
//            serviceProviderMock.Setup(t => t.GetService(It.Is<Type>(i => i == typeof(ITracingService)))).Returns(tracingService);
//            serviceProviderMock.Setup(t => t.GetService(It.Is<Type>(i => i == typeof(IOrganizationServiceFactory)))).Returns(factory);
//            serviceProviderMock.Setup(t => t.GetService(It.Is<Type>(i => i == typeof(IPluginExecutionContext)))).Returns(pluginContext);
//            var serviceProvider = serviceProviderMock.Object;

//            //PostCreateSMS PostCreateSMS = new PostCreateSMS();
//            //PostCreateSMS.Execute(serviceProvider);

//            //ValidateCommentRecordExist PortalNotification = new ValidateCommentRecordExist();
//            DeleteCategory mapTransactionOnTicketCreation = new DeleteCategory();
//            mapTransactionOnTicketCreation.Execute(serviceProvider);

//        }
//        public static IOrganizationService GetCrmConnection(string uri, string userName, string password)
//        {
//            IOrganizationService organizationService = null;
//            //try
//            {
//                Uri organizationUri = new Uri(uri);
//                ClientCredentials clientCredentials = new ClientCredentials();
//                clientCredentials.UserName.UserName = userName;
//                clientCredentials.UserName.Password = password;
//                OrganizationServiceProxy Proxy = new OrganizationServiceProxy(organizationUri, null, clientCredentials, null);
//                organizationService = Proxy as IOrganizationService;
//                return organizationService;
//            }
//            //catch (Exception ex)
//            //{
//            //    return organizationService;
//            //    throw new Exception(ex.Message);
//            //}
//        }
//    }
//}

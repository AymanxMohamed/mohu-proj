//using LinkDev.Common.Crm.Cs.StageConfiguration.Entities;
//using LinkDev.Common.Crm.Cs.StageConfiguration.Enum;
//using LinkDev.CRM.Library.DAL;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Query;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LinkDev.Common.Crm.Cs.StageConfiguration.BLL
//{
//    public class ResettingStageFieldsBLL
//    {
//        private IOrganizationService OrganizationService;
//        //private CrmLog log;
//        private CRMAccessLayer crmAccess;
//        StageConfigurationBLL logicLayer;

//        /// <summary>
//        /// the constractor which have service and log as input parameter
//        /// </summary>
//        /// <param name="service"></param>
//        /// <param name="log"></param>
//        public ResettingStageFieldsBLL(IOrganizationService service)//,CrmLog log)
//        {
//            OrganizationService = service;
//            crmAccess = new CRMAccessLayer(OrganizationService);
//            // this.log = log;
//            logicLayer = new StageConfigurationBLL(service);
//        }
//        public void ResetRequestFields(EntityReference stageConfiguration, EntityReference applicationHeader)
//        {
//            if (stageConfiguration?.Id !=Guid.Empty && applicationHeader?.Id != Guid.Empty)
//            {
//                List<StageFields> stageFields = logicLayer.RetrieveStageFields(stageConfiguration.Id,GridType.FieldsToBeResitting);      
//                //     //In case it's a field has schemaName comma seprated
//                //     Entity stageConfigurationEntity = crmAccess.RetrieveEntity(stageConfiguration.Id, stageConfiguration.LogicalName, new[] {StageConfigurationEntity.ResetFieldsSchemaName });
//                //string[] fieldsToReset = stageConfigurationEntity.Contains(StageConfigurationEntity.ResetFieldsSchemaName)? stageConfigurationEntity.GetAttributeValue<string>(StageConfigurationEntity.ResetFieldsSchemaName).Trim().ToString().Split(','):null;
                
//                ////Get application Header
//                Entity applicationHeaderEntity = crmAccess.RetrieveEntity(applicationHeader.Id, applicationHeader.LogicalName, new[] { ApplicationHeaderEntity.RelatedApplicationId ,ApplicationHeaderEntity.RelatedApplicationSchemaName  } );
//                //Get Request Entity
//                if (applicationHeaderEntity?.Id!=Guid.Empty && stageFields.Count > 0)
//                {
//                    string requestSchemaName = applicationHeaderEntity.Contains(ApplicationHeaderEntity.RelatedApplicationSchemaName) ? applicationHeaderEntity.GetAttributeValue<string>(ApplicationHeaderEntity.RelatedApplicationSchemaName) : null;
//                    string requestId = applicationHeaderEntity.Contains(ApplicationHeaderEntity.RelatedApplicationId) ? applicationHeaderEntity.GetAttributeValue<string>(ApplicationHeaderEntity.RelatedApplicationId) : null;
//                    Entity request = new Entity(requestSchemaName, new Guid(requestId));
//                    foreach (StageFields fieldToReset in stageFields)
//                    {
//                        request.Attributes[fieldToReset.FieldSchemaName] = null;
//                    }
//                    crmAccess.UpdateEntity(request);
//                }
//            }
//        }
//    }
//}

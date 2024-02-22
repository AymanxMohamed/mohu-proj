using LinkDev.Common.Crm.Cs.StageConfiguration.Entities;
using LinkDev.Common.Crm.Cs.StageConfiguration.Enum;
using Linkdev.CRM.CS.s.StageConfiguration.Entities;
using LinkDev.CRM.Library.DAL;
using Microsoft.Xrm.Sdk;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Common.Crm.Bll.Base;
using Microsoft.Xrm.Sdk.Query;
using LinkDev.Common.Crm.Logger;
using SeverityLevel = LinkDev.Common.Crm.Logger.SeverityLevel;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace LinkDev.Common.Crm.Cs.StageConfiguration.BLL
{
    public class HistoricallyStageFieldsBLL  
    {
        private IOrganizationService OrganizationService;
        private CRMAccessLayer crmAccess;
        ITracingService tracingService;
        StageConfigurationBLL logicLayer;
        public HistoricallyStageFieldsBLL(IOrganizationService service, ITracingService tracingServices)
        {
            OrganizationService = service;
            crmAccess = new CRMAccessLayer(OrganizationService);
            tracingService = tracingServices;
            logicLayer = new StageConfigurationBLL(OrganizationService);
        }

        public void FieldsToBeHistorically(EntityReference stageConfiguration, EntityReference applicationHeader)
        {
            tracingService.Trace("In FieldsToBeHistorically ");

            if (stageConfiguration?.Id == Guid.Empty && applicationHeader?.Id == Guid.Empty) return;

            //Get application Header
            var applicationHeaderEntity = OrganizationService.Retrieve(applicationHeader.LogicalName, applicationHeader.Id, new ColumnSet(new[] { ApplicationHeaderEntity.Regarding }));
            if (applicationHeaderEntity == null || applicationHeaderEntity?.Id == Guid.Empty) return;

            //Get Request Entity
            EntityReference Regarding = applicationHeaderEntity.Contains(ApplicationHeaderEntity.Regarding) ? applicationHeaderEntity.GetAttributeValue<EntityReference>(ApplicationHeaderEntity.Regarding) : null;
            if (Regarding == null) return;
            Entity requestEntity = new Entity(Regarding.LogicalName, Regarding.Id);
            tracingService.Trace($"requestEntity {requestEntity.LogicalName} , {requestEntity.Id}");

            // commented and replaced to accept condition AMR Ezzat
            //List<StageFields> stageFields = logicLayer.RetrieveStageFields(stageConfiguration.Id, GridType.FieldsToBeHistorically, requestEntity);
            //if (stageFields.Count <= 0) return;

            //Logger.LogComment(LoggerHandler.GetMethodFullName(), $"stageFields.Count = {stageFields.Count}", SeverityLevel.Info);
            tracingService.Trace($"stageConfiguration.Id {stageConfiguration.Id} , GridType.FieldsToBeHistorically {GridType.FieldsToBeHistorically} ,requestEntity {requestEntity.LogicalName} , {requestEntity.Id} ");

            tracingService.Trace($" 1 ");

            //Get Stage Fields
            List<ChangedFieldTriggers> changedFieldTriggers = logicLayer.RetrieveChangedStageFields(stageConfiguration.Id, GridType.FieldsToBeHistorically, requestEntity, tracingService);
            tracingService.Trace($" changedFieldTriggers.Count");

            if (changedFieldTriggers.Count<1) return;
            tracingService.Trace($"changedFieldTriggers.Count = {changedFieldTriggers.Count}");
            List<StageFields> stageFields = ValidStageFieldsList(changedFieldTriggers, requestEntity, tracingService);
            if (stageFields.Count <= 0) return;
            tracingService.Trace($"stageFields.Count = {stageFields.Count}");

            //get all fields that need to be historically from request
            string[] historicallyColumns = new string[stageFields.Count + 1];
            int columnsCount = 1;
            historicallyColumns[0] = RequestEntity.CurrentTask;
            Entity currentTask = new Entity();
            foreach (StageFields historicallyFields in stageFields)
            {
                if (historicallyFields.FieldSchemaName != string.Empty)
                {
                    tracingService.Trace($"historicallyFields.FieldSchemaName = {historicallyFields.FieldSchemaName }");

                    //check if field exist in request first
                    bool doesFieldExistInTargetEntity = crmAccess.DoesFieldExist(Regarding.LogicalName, historicallyFields.FieldSchemaName);
                    tracingService.Trace($"doesFieldExistInTargetEntity = {doesFieldExistInTargetEntity}");

                    if (doesFieldExistInTargetEntity)
                    {
                        historicallyColumns[columnsCount] = historicallyFields.FieldSchemaName;
                        columnsCount++;
                    }
                }
            }
            if (!historicallyColumns.Any()) return;
            Entity request = crmAccess.RetrieveEntityWithColumns(Regarding, historicallyColumns);
            if (request.Contains(RequestEntity.CurrentTask))
            {
                EntityReference task = request.Contains(RequestEntity.CurrentTask) ? request.GetAttributeValue<EntityReference>(RequestEntity.CurrentTask) : new EntityReference();
                if (task?.Id != Guid.Empty)
                {

                    currentTask = new Entity(TaskEntity.LogicalName, task.Id);
                    foreach (StageFields historicallyFieldsValue in stageFields)
                    {
                        tracingService.Trace($"historicallyFieldsValue = {historicallyFieldsValue.FieldSchemaName } , historicallyFieldsValue.FieldType.Id {historicallyFieldsValue.FieldType.Id} , historicallyFieldsValue.TaskFieldSchemaName  {historicallyFieldsValue.TaskFieldSchemaName }");
                        tracingService.Trace($"currentTask.Attributes.Count {currentTask.Attributes.Count}");

                        if (historicallyFieldsValue.FieldType.Id == (int)FieldType.Lookup) //Decision Made By, user 
                        {
                            tracingService.Trace($"Lookup");

                            if (request.Contains(historicallyFieldsValue.FieldSchemaName))
                            {
                                if (historicallyFieldsValue.TaskFieldSchemaName != string.Empty)
                                {
                                    if (request.Contains(historicallyFieldsValue.FieldSchemaName))
                                    {
                                        if ((request.GetAttributeValue<EntityReference>(historicallyFieldsValue.FieldSchemaName)).LogicalName=="team")
                                        {
                                            currentTask.Attributes.Add(historicallyFieldsValue.TaskFieldSchemaName,
                                                                                  request.GetAttributeValue<EntityReference>(historicallyFieldsValue.FieldSchemaName));

                                        }

                                        tracingService.Trace($" logical name {(request.GetAttributeValue<EntityReference>(historicallyFieldsValue.FieldSchemaName)).LogicalName} , id : {(request.GetAttributeValue<EntityReference>(historicallyFieldsValue.FieldSchemaName)).Id }");
                                        tracingService.Trace($"lookup value: {   request.GetAttributeValue<EntityReference>(historicallyFieldsValue.FieldSchemaName)  }  ");

                                    }

                                }
                            }
                        }
                        else  if (historicallyFieldsValue.FieldType.Id == (int)FieldType.Optionset) //decision
                        {
                            tracingService.Trace($"Optionset");

                            if (historicallyFieldsValue.TaskFieldSchemaName != string.Empty)
                            {

                                // changed to retrive arabic values 
                                //var desicion = request.Contains(historicallyFieldsValue.FieldSchemaName) ? request.FormattedValues[historicallyFieldsValue.FieldSchemaName] : null;// request.FormattedValues[historicallyFieldsValue.FieldSchemaName];
                                var desicion = GetAttributeArabicValueRequest(request.GetAttributeValue<OptionSetValue>(historicallyFieldsValue.FieldSchemaName).Value, 1025, request.LogicalName, historicallyFieldsValue.FieldSchemaName);
                                if (string.IsNullOrEmpty( desicion))
                                {
                                      desicion = GetAttributeArabicValueRequest(request.GetAttributeValue<OptionSetValue>(historicallyFieldsValue.FieldSchemaName).Value, 1033, request.LogicalName, historicallyFieldsValue.FieldSchemaName);
                                }
                                tracingService.Trace($"desicion {desicion}");
                                if (!string.IsNullOrEmpty(desicion))
                                {
                                    currentTask.Attributes.Add(historicallyFieldsValue.TaskFieldSchemaName, desicion);
                                }
                            }
                        }
                        else //comment string
                        {
                            tracingService.Trace($" string");
                            if (historicallyFieldsValue.TaskFieldSchemaName != string.Empty)
                            {
                               if (request.Contains(historicallyFieldsValue.FieldSchemaName))
                                {
                                    var comment =  request.Attributes[historicallyFieldsValue.FieldSchemaName]  ;
                                    currentTask.Attributes.Add(historicallyFieldsValue.TaskFieldSchemaName, comment);
                                    tracingService.Trace($"comment {comment}");
                                }
                            }
                        }
                    }
                }
            }
            tracingService.Trace($"before update task ");
            tracingService.Trace( LoggerHandler.CommentAttributeCollection(currentTask.Attributes));

            crmAccess.UpdateEntity(currentTask);
            tracingService.Trace($"'{currentTask.LogicalName}' with Id '{currentTask.Id}' updated with historical data as following { LoggerHandler.CommentAttributeCollection(currentTask.Attributes)} ");
        }

        public string GetAttributeArabicValueRequest(int optSetValue, int LanguageCode, string entityLogicalName, string attributeName)
        {
            tracingService.Trace($" in GetAttributeArabicValueRequest ");
            var value = OptionSetMetaData(entityLogicalName, attributeName, optSetValue);
            //RetrieveAttributeRequest attributeRequest = new RetrieveAttributeRequest
            //{
            //    EntityLogicalName = entityLogicalName,
            //    LogicalName = attributeName,
            //    RetrieveAsIfPublished = true
            //};

            //var response = (RetrieveAttributeResponse)OrganizationService.Execute(attributeRequest);
            //var optionList = ((EnumAttributeMetadata)response.AttributeMetadata).OptionSet.Options;
            //tracingService.Trace($"optionList {optionList} ,LanguageCode {LanguageCode}");
            //var value= optionList.FirstOrDefault(o => o.Value == optSetValue).Label.LocalizedLabels.First(l => l.LanguageCode == LanguageCode).Label;
            //tracingService.Trace($"value {value}");

            return value;
        }
        string  OptionSetMetaData(string logicalName, string fieldInApplication, int fieldOptionNumber)
        {
            string outPutValue = "";
            RetrieveAttributeRequest raRequest = new RetrieveAttributeRequest
            {
                EntityLogicalName = logicalName,
                LogicalName = fieldInApplication,
                RetrieveAsIfPublished = true
            };
            RetrieveAttributeResponse raResponse = (RetrieveAttributeResponse)OrganizationService.Execute(raRequest);
            PicklistAttributeMetadata paMetadata = (PicklistAttributeMetadata)raResponse.AttributeMetadata;
            OptionMetadata[] optionList = paMetadata.OptionSet.Options.ToArray();
            foreach (OptionMetadata oMD in optionList)
            {
                //int value = Int.TryParse(optionSetvalue);
                if (oMD.Value == fieldOptionNumber)
                {
                    // log.LogInfo($"field Option language lcid both ");
                    outPutValue = oMD.Label.LocalizedLabels.Where(x => x.LanguageCode == 1033).FirstOrDefault().Label.ToString();
                    tracingService.Trace($"oMD.Value {oMD.Value} ,   field {fieldInApplication}  English  {outPutValue}, optionSetValue {outPutValue}  ");
                }
            }
            return outPutValue;
        }
                public List<StageFields> ValidStageFieldsList(List<ChangedFieldTriggers> changedFieldTriggers, Entity request, ITracingService tracingService)
        {
            List<StageFields> stageFieldsList = new List<StageFields>();
            if (changedFieldTriggers.Any())
            {
                foreach (ChangedFieldTriggers changedFieldPerSchemaName in changedFieldTriggers)
                {
                    int counter = changedFieldPerSchemaName.StageFields.Count;
                    if (changedFieldPerSchemaName != null)
                    {
                        StageFields firstStageFields = changedFieldPerSchemaName.StageFields[0];

                        #region 1- only one with condition check is valid then update it
                        if (counter == 1 && firstStageFields.HasCondition)
                        {
                            tracingService.Trace($"1- withCondition == 1 && emptyCondition == 0 ");

                            //check if condition met
                            if (firstStageFields.ConditionField != null && firstStageFields.ConditionField.FetchCondition != string.Empty && request.LogicalName == firstStageFields.EntitySchemaName)
                            {
                                firstStageFields.ConditionField.IsConditionMet = crmAccess.IsConditionMet(firstStageFields.ConditionField.FetchCondition, request.ToEntityReference(), false);
                                tracingService.Trace($" Is condition? {firstStageFields.ConditionField.IsConditionMet } ");
                                if (firstStageFields.ConditionField.IsConditionMet)
                                {
                                    stageFieldsList.Add(firstStageFields);
                                }
                                else
                                {
                                    tracingService.Trace($" Condition didn't met ");
                                }
                            }
                            else
                            {
                                tracingService.Trace($"Wrong in Condition or schema Name ");
                            }
                        }
                        #endregion

                        #region 2- only one without condition then update it
                        else if (counter == 1 && !firstStageFields.HasCondition)
                        {
                            tracingService.Trace($"in ---->2-  emptyCondition == 1 && withCondition == 0  ");
                            stageFieldsList.Add(firstStageFields);
                        }
                        #endregion

                        #region 3-if one without condition and one or more with condition ,update the one without condition in case all other condition not valid
                        else if (changedFieldPerSchemaName.WithConditionCount >= 1 && changedFieldPerSchemaName.WithOutConditionCount == 1)
                        {
                            tracingService.Trace($"in ----->3- emptyCondition == 1 && withCondition >= 1  ");
                            int validConditionsCount = 0;
                            StageFields stageFieldsWithNoCondition = new StageFields();
                            foreach (StageFields stageFields in changedFieldPerSchemaName.StageFields)
                            {
                                //condition exist
                                if (stageFields.HasCondition)
                                {
                                    if (stageFields.ConditionField != null && stageFields.ConditionField.FetchCondition != string.Empty && request.LogicalName == stageFields.EntitySchemaName)
                                    {
                                        stageFields.ConditionField.IsConditionMet = crmAccess.IsConditionMet(stageFields.ConditionField.FetchCondition, request.ToEntityReference(), false);
                                        tracingService.Trace($" Is condition? {stageFields.ConditionField.IsConditionMet } ");
                                        if (stageFields.ConditionField.IsConditionMet)
                                        {
                                            validConditionsCount++;
                                            if (validConditionsCount >= 2)
                                            {
                                                tracingService.Trace($"more than only One Valid Condition ");
                                                break;
                                            }
                                        }
                                    }
                                }
                                else //no condition
                                {
                                    stageFieldsWithNoCondition = stageFields;
                                }
                            }
                            if (validConditionsCount == 0)
                            {
                                stageFieldsList.Add(stageFieldsWithNoCondition);
                            }
                        }
                        #endregion

                        #region 4- if there are more than one condition ,update only in case one condition is valid and other not valid
                        else if (changedFieldPerSchemaName.WithConditionCount > 0 && changedFieldPerSchemaName.WithOutConditionCount == 0)
                        {
                            StageFields validStage = new StageFields();
                            tracingService.Trace($"in -----> emptyCondition == 0 && withCondition > 0 ");
                            int onlyOneValid = 0;
                            foreach (StageFields stageFields in changedFieldPerSchemaName.StageFields)
                            {
                                if (stageFields.HasCondition)
                                {
                                    if (stageFields.ConditionField != null && stageFields.ConditionField.FetchCondition != string.Empty && request.LogicalName == stageFields.EntitySchemaName)
                                    {
                                        stageFields.ConditionField.IsConditionMet = crmAccess.IsConditionMet(stageFields.ConditionField.FetchCondition, request.ToEntityReference(), false);
                                        tracingService.Trace($" Is condition? {stageFields.ConditionField.IsConditionMet } ");
                                        if (stageFields.ConditionField.IsConditionMet)
                                        {
                                            // check  if more than one condition valid break ;  
                                            onlyOneValid++;
                                            if (onlyOneValid == 1)
                                            {
                                                //index of it
                                                validStage = stageFields;
                                            }
                                            else if (onlyOneValid >= 2)
                                            {
                                                tracingService.Trace($"more than only One Valid Condition ");
                                                break;
                                            }
                                        }
                                    }
                                }

                            }
                            if (onlyOneValid == 1)
                            {
                                stageFieldsList.Add(validStage);
                            }
                        }
                        #endregion

                    }
                }
            }
            return stageFieldsList;
        }

    }
}

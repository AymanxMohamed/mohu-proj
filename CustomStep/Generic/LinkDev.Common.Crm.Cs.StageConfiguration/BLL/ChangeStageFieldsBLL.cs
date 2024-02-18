using LinkDev.Common.Crm.Cs.StageConfiguration.Entities;
using LinkDev.Common.Crm.Cs.StageConfiguration.Enum;
using LinkDev.CRM.Library.DAL;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.StageConfiguration.BLL
{
    public class ChangeStageFieldsBLL
    {
        private IOrganizationService OrganizationService;
        private CRMAccessLayer crmAccess;
        StageConfigurationBLL logicLayer;

        public ChangeStageFieldsBLL(IOrganizationService service, ITracingService tracingService, CodeActivityContext executionContext)
        {
                OrganizationService = service;
                crmAccess = new CRMAccessLayer(OrganizationService);
            logicLayer = new StageConfigurationBLL(service, tracingService, executionContext);
        }       
        public void FieldsToBeChanged(EntityReference stageConfiguration, EntityReference applicationHeader, ITracingService tracingService)
        {
            if (stageConfiguration?.Id == Guid.Empty && applicationHeader?.Id == Guid.Empty) return;                      
            //Get application Header
            Entity applicationHeaderEntity = crmAccess.RetrieveEntity(applicationHeader.Id, applicationHeader.LogicalName, new[] { ApplicationHeaderEntity.Regarding });
            if (applicationHeaderEntity?.Id == Guid.Empty) return;
            //Get Request Entity
            EntityReference Regarding = applicationHeaderEntity.Contains(ApplicationHeaderEntity.Regarding) ? applicationHeaderEntity.GetAttributeValue<EntityReference>(ApplicationHeaderEntity.Regarding) : null;
            if (Regarding == null) return;
            Entity request = new Entity(Regarding.LogicalName, Regarding.Id);

            //old 
            //List<StageFields> stageFields = logicLayer.RetrieveStageFields(stageConfiguration.Id, GridType.FieldsToBeChanged, request, tracingService);

            ////Get Stage Fieldslisted per SchemaName
            List<ChangedFieldTriggers> changedFieldTriggers = logicLayer.RetrieveChangedStageFields(stageConfiguration.Id, GridType.FieldsToBeChanged, request, tracingService);
            if (!changedFieldTriggers.Any()) return;
            List<StageFields> stageFields = ValidStageFieldsList(changedFieldTriggers, request, tracingService);
            if (stageFields.Count <= 0) {
                tracingService.Trace($"stageFields.Count = {stageFields.Count}");
                return; }
            tracingService.Trace($"stageFields.Count = {stageFields.Count}");
            foreach (StageFields fieldsToBeChanged in stageFields)
            {
                tracingService.Trace($"fieldsToBeChanged.IsNullable= {fieldsToBeChanged.IsNullable}");
                //1-check the nullable
                if (fieldsToBeChanged.IsNullable)
                {
                    if (fieldsToBeChanged.FieldSchemaName != string.Empty)
                    {
                        //if (request.Attributes[fieldsToBeChanged.FieldSchemaName] is BooleanManagedProperty)
                        //{
                        //    request.Attributes[fieldsToBeChanged.FieldSchemaName] = false;
                        //    tracingService.Trace($"Clear Field {fieldsToBeChanged.FieldSchemaName}");
                        //}
                        //else
                        {
                            request.Attributes[fieldsToBeChanged.FieldSchemaName] = null;
                            tracingService.Trace($"Clear Field {fieldsToBeChanged.FieldSchemaName}");
                        }                       
                    }                    
                }
                else if (fieldsToBeChanged.FieldType.Id == (int)FieldType.Lookup)
                {
                    string fieldschemaName = fieldsToBeChanged.FieldSchemaName;
                    string lookupEntitySchemaname = fieldsToBeChanged.LookupEntitySchemaName;
                    int codeValue = Int32.Parse(fieldsToBeChanged.CodeValue);
                    Guid lookupGuid = logicLayer.GetEntityByCode(codeValue, lookupEntitySchemaname);
                    //set it
                    if (lookupGuid != Guid.Empty && lookupEntitySchemaname != string.Empty && lookupGuid != null && lookupEntitySchemaname != null)
                    {
                        request.Attributes[fieldsToBeChanged.FieldSchemaName] = new EntityReference(lookupEntitySchemaname, lookupGuid);
                        tracingService.Trace($"Field is lookup {fieldsToBeChanged.FieldSchemaName} value {lookupGuid}");
                    }
                }
                else if (fieldsToBeChanged.FieldType.Id == (int)FieldType.Optionset)
                {
                    if (fieldsToBeChanged.CodeValue != string.Empty)
                    {
                        request.Attributes[fieldsToBeChanged.FieldSchemaName] = new OptionSetValue(Int32.Parse(fieldsToBeChanged.CodeValue));
                        tracingService.Trace($"Field is optionset{fieldsToBeChanged.FieldSchemaName} value {fieldsToBeChanged.CodeValue}");
                    }                   
                }
                else if (fieldsToBeChanged.FieldType.Id == (int)FieldType.TwoOption)
                {
                    if (fieldsToBeChanged.CodeValue != string.Empty)
                    {
                        request.Attributes[fieldsToBeChanged.FieldSchemaName] = bool.Parse(fieldsToBeChanged.CodeValue);
                        tracingService.Trace($"Field is Two Option {fieldsToBeChanged.FieldSchemaName} value {fieldsToBeChanged.CodeValue}");
                    }
                }
                //else
                //{
                //    if (fieldsToBeChanged.CodeValue != string.Empty)
                //    {
                //        request.Attributes[fieldsToBeChanged.FieldSchemaName] =  fieldsToBeChanged.CodeValue;
                //        tracingService.Trace($"Field is optionset{fieldsToBeChanged.FieldSchemaName} value {fieldsToBeChanged.CodeValue}");
                //    }
                //}
            }
            crmAccess.UpdateEntity(request);
            tracingService.Trace($"request updated");
        }
        public void CheckFieldType(Entity entity, string fieldName)
        {

            //if (entity.Attributes.Contains(fieldName))
            //{
            //    if (entity.Attributes[fieldName] is String) //String
            //        return ((String)entity.Attributes[fieldName]).ToString();

            //    else if (entity.Attributes[fieldName] is OptionSetValue) //OptionSet
            //    {
            //        if (entity.FormattedValues.Contains(fieldName))
            //            return (string)entity.FormattedValues[fieldName];
            //        else
            //            return ((OptionSetValue)entity.Attributes[fieldName]).Value.ToString();
            //    }

            //    else if (entity.Attributes[fieldName] is DateTime) //DateTime
            //        return ((DateTime)entity.Attributes[fieldName]).ToString();

            //    else if (entity.Attributes[fieldName] is BooleanManagedProperty) //Boolean
            //        return ((BooleanManagedProperty)entity.Attributes[fieldName]).Value.ToString();

            //    else if (entity.Attributes[fieldName] is float) //float
            //        return ((float)entity.Attributes[fieldName]).ToString();

            //    else if (entity.Attributes[fieldName] is int) //Integer
            //        return ((int)entity.Attributes[fieldName]).ToString();

            //    else if (entity.Attributes[fieldName] is EntityReference) //Lookup
            //        return ((EntityReference)entity.Attributes[fieldName]).Name;

            //    else if (entity.Attributes[fieldName] is Money) //Crm Money
            //        return ((Money)entity.Attributes[fieldName]).Value.ToString();

            //    else if (entity.Attributes[fieldName] is decimal) //Decimal
            //        return ((decimal)entity.Attributes[fieldName]).ToString();

            //    else
            //        return "";
            //}
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

using LinkDev.MAAN.Common;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Steps.MiniStageConfiguration.Logic
{
    public class LogFieldsLogic : StepLogic<LogFields>
    {
        protected override void ExecuteLogic()
        {
            tracingService.Trace($"  LogFieldsLogic");
            log.LogInfo($" LogFieldsLogic");
            string fieldsToBeLogged = codeActivity.FieldsToBeLogged.Get(executionContext);
            log.LogInfo($" fieldsToBeLogged {fieldsToBeLogged}");
            tracingService.Trace($" fieldsToBeLogged {fieldsToBeLogged}");

            bool isApplicantLogged = codeActivity.IsApplicantLogged.Get(executionContext);
            log.LogInfo($" isApplicantLogged {isApplicantLogged}");
            tracingService.Trace($" isApplicantLogged {isApplicantLogged}");

            string actionOrDecisionType = codeActivity.ActionOrDecisionType.Get(executionContext);
            log.LogInfo($" actionOrDecisionType {actionOrDecisionType}  ");

            string twoOptionValue = codeActivity.TwoOptionValue.Get(executionContext);
            bool isTwoOption = codeActivity.IsTwoOption.Get(executionContext);
            if (isTwoOption && string.IsNullOrEmpty(twoOptionValue))
            {
                throw new Exception(string.Format($"  must have value in twoOption Value as you added isTwoOption=true "));
            }
            log.LogInfo($" isTwoOption {isTwoOption} with value {twoOptionValue}");
            tracingService.Trace($" isTwoOption {isTwoOption} with value {twoOptionValue}");

            EntityReference entity = new EntityReference();
            if (codeActivity.IncubationApplication.Get(executionContext) != null)
            {
                entity = codeActivity.IncubationApplication.Get(executionContext);
            }
            else
            {
                entity = new EntityReference(codeActivity.EntityLogicalName.Get(executionContext), new Guid(codeActivity.EntityId.Get(executionContext)));
            }

            Guid id = Guid.Empty;
            string entitySchemaName = string.Empty;
            if (entity != null && entity?.Id != null && entity?.Id != Guid.Empty)
            {
                id = entity.Id;
                entitySchemaName = entity.LogicalName;
            }
            else
            {
                id = context.PrimaryEntityId;
                entitySchemaName = context.PrimaryEntityName;
            }
            tracingService.Trace($"1 target Entity id : {id} , schemname {entitySchemaName}");
            log.LogInfo($"2 target Entity id : {id} , schemname {entitySchemaName}");

            EntityReference targetEntity = new EntityReference(entitySchemaName, id);
            LogFields(targetEntity, fieldsToBeLogged, isApplicantLogged, actionOrDecisionType, isTwoOption, twoOptionValue);
        }

        public void LogFields(EntityReference targetEntity, string FieldsToLog, bool isApplicantLogged,string actionOrDecisionType, bool isTwoOption, string twoOptionValue)
        {
            tracingService.Trace($"1 LogFields");
            log.LogInfo($" * LogFields");

            Entity LogHistory = new Entity("ldv_applicationactionlog");
            string[] FieldsList = FieldsToLog.Split(',');
           // log.LogInfo($"{FieldsList}");

            string columns = string.Empty;
            ColumnSet ColumnSet = new ColumnSet();
            foreach (string dynamicValue in FieldsList)
            {
                log.LogInfo($"dynamicValue : {dynamicValue}");
                if (dynamicValue.Contains(":"))
                {
                   // log.LogInfo($"ColumnSet.Columns.Count {ColumnSet.Columns.Count} ");
                    string[] splitedDynamicValue = dynamicValue.Split(':');
                    string fieldName = splitedDynamicValue[0];
                    if (fieldName!=null)
                    {
                        ColumnSet.AddColumn(fieldName);
                       // log.LogInfo($"ColumnSet.Columns.Count {ColumnSet.Columns.Count} ");

                    }
                }
            }
            log.LogInfo($"ColumnSet.Columns.Count {ColumnSet.Columns.Count} ");

            if (ColumnSet.Columns.Count>0 || FieldsToLog.ToLower() == "submit")
            {
                    ColumnSet.AddColumn("ownerid");
                if (isApplicantLogged)
                {
                    ColumnSet.AddColumn("ldv_customerid");
                }
                if (   isTwoOption  && !string.IsNullOrEmpty( twoOptionValue))
                {
                    LogHistory.Attributes.Add("ldv_action", twoOptionValue);
                }
                Entity currentEntity = service.Retrieve(targetEntity.LogicalName, targetEntity.Id, ColumnSet);
                if (currentEntity != null && currentEntity?.Id != null)
                {
                    foreach (string dynamicValue in FieldsList)
                    {
                        //if (FieldsToLog == "submit")
                        //{
                        //    LogHistory.Attributes.Add("ldv_action", "submitted");

                        //}
                        //else
                        if (dynamicValue.Contains(":"))
                        {
                            string[] splitedDynamicValue = dynamicValue.Split(':');
                            string fieldInApplication = splitedDynamicValue[0];
                            string fieldInLog = splitedDynamicValue[1];
                            log.LogInfo($"fieldInApplication {fieldInApplication} , fieldInLog {fieldInLog} ");
                            if (currentEntity.Contains(fieldInApplication))
                            {
                                var value = currentEntity[fieldInApplication];
                                log.LogInfo($"value of fieldInApplication :  {value} ");

                                //check type if option set
                                //Type fieldType = (currentEntity.Attributes[fieldInApplication]).GetType();
                                #region optionset
                                if (currentEntity.Attributes[fieldInApplication] is OptionSetValue) //OptionSet
                                {
                                    log.LogInfo($"Field is OptionSet ");
                                    int fieldOptionNumber = currentEntity.GetAttributeValue<OptionSetValue>(fieldInApplication).Value;
                                 
                                    log.LogInfo($"Field is fieldOptionNumber {fieldOptionNumber} ");

                                    if ( (isTwoOption && fieldInLog != "ldv_action") || (!isTwoOption && fieldInLog == "ldv_action") || ( fieldInLog != "ldv_action"))
                                    {
                                        if (!string.IsNullOrEmpty(actionOrDecisionType) && fieldInLog == "ldv_action")
                                        {
                                            LogHistory.Attributes.Add(fieldInLog, actionOrDecisionType);
                                            log.LogInfo($"will log field {fieldInApplication}  in {fieldInLog} with value : {actionOrDecisionType} ");
                                        }
                                        else
                                        {
                                            log.LogInfo($"field Option Value {fieldOptionNumber} ");
                                            //  log.LogInfo($"field Option string { currentEntity.GetAttributeValue<OptionSetValue>(fieldInApplication).ToString()} ");
                                            //var optionSetFieldValue = LocalOptionSetList(currentEntity.LogicalName, fieldInApplication optionSetName, fieldOptionNumber optionSetvalue, 0);
                                            #region metadata
                                            RetrieveAttributeRequest raRequest = new RetrieveAttributeRequest
                                            {
                                                EntityLogicalName = currentEntity.LogicalName,
                                                LogicalName = fieldInApplication,
                                                RetrieveAsIfPublished = true
                                            };
                                            RetrieveAttributeResponse raResponse = (RetrieveAttributeResponse)service.Execute(raRequest);
                                            PicklistAttributeMetadata paMetadata = (PicklistAttributeMetadata)raResponse.AttributeMetadata;
                                            OptionMetadata[] optionList = paMetadata.OptionSet.Options.ToArray();
                                            foreach (OptionMetadata oMD in optionList)
                                            {
                                                //int value = Int.TryParse(optionSetvalue);
                                                if (oMD.Value == fieldOptionNumber)
                                                {
                                                    string outPutValue = "";

                                                    // log.LogInfo($"field Option language lcid both ");
                                                    outPutValue = oMD.Label.LocalizedLabels.Where(x => x.LanguageCode == 1033).FirstOrDefault().Label.ToString();
                                                    log.LogInfo($"English  {outPutValue}");
                                                    //log.LogInfo($"English  {oMD.Label.LocalizedLabels.Where(x => x.LanguageCode == 1033).FirstOrDefault().Label.ToString()}");
                                                    //log.LogInfo($"Arabic { oMD.Label.LocalizedLabels.Where(x => x.LanguageCode == 1025).FirstOrDefault().Label.ToString()}");
                                                    //  + " - " + oMD.Label.LocalizedLabels.Where(x => x.LanguageCode == 1025).FirstOrDefault().Label.ToString();
                                                    log.LogInfo($"optionSetValue {outPutValue}   ");
                                                    if (outPutValue != null)
                                                    {
                                                        LogHistory.Attributes.Add(fieldInLog, outPutValue);
                                                        log.LogInfo($"will log field {fieldInApplication}  in {fieldInLog}  with value : {outPutValue}");
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        log.LogInfo($"Field is fieldInApplication {fieldInApplication} not in condition :D ");

                                    }
                                }
                                #endregion
                                else
                                {
                                    LogHistory.Attributes.Add(fieldInLog, value);
                                    log.LogInfo($"will log field {fieldInApplication}  in {fieldInLog} ");
                                }
                            }
                        }
                         
                    }
                    Entity applicationEntity = new Entity(targetEntity.LogicalName, targetEntity.Id);
                    applicationEntity.Attributes.Add("ldv_decisiondate", DateTime.Now);
                   
                    LogHistory.Attributes.Add("ldv_actiondate", DateTime.Now);
                    LogHistory.Attributes.Add("ldv_requestid", targetEntity);

                    if (isApplicantLogged)
                    {
                        LogHistory.Attributes.Add("ldv_actionbycode", new OptionSetValue(2));//applicant 
                        LogHistory.Attributes.Add("ldv_actiontakenbyid", currentEntity["ldv_customerid"]);//applicant
                        applicationEntity.Attributes.Add("ldv_actiontakenbyid", currentEntity["ldv_customerid"]);
                        //LogHistory.Attributes.Add("ldv_action", "Submitted");
                        log.LogInfo($"isApplicantLogged {isApplicantLogged} ");
                    }
                    else
                    {
                        LogHistory.Attributes.Add("ldv_actionbycode", new OptionSetValue(1));//maan
                        LogHistory.Attributes.Add("ldv_actiontakenbyid", currentEntity["ownerid"]);//maan
                        applicationEntity.Attributes.Add("ldv_actiontakenbyid", currentEntity["ownerid"]);
                        log.LogInfo($"isApplicantLogged {isApplicantLogged} ");
                    }

                    foreach (var item in LogHistory.Attributes)
                    {
                        log.LogInfo($"LogHistory.Attributes {item.Key} ");
                        log.LogInfo($"LogHistory.Attributes {item.Value} ");
                    }

                    Guid id = service.Create(LogHistory);
                    tracingService.Trace($"log record created successfuly with id :{id} ");
                    log.LogInfo($"log record created successfuly with id : {id}  ");


                    /// update application
                    service.Update(applicationEntity);
                    log.LogInfo($"updated ldv_decisiondate and ldv_actiontakenbyid in application {targetEntity.LogicalName} , {targetEntity.Id}");

                }
            }
        }
        public string LocalOptionSetList(String entityName, String optionSetName, int optionSetvalue, int lcid)
        {
            try
            {
                RetrieveAttributeRequest raRequest = new RetrieveAttributeRequest
                {
                    EntityLogicalName = entityName,
                    LogicalName = optionSetName,
                    RetrieveAsIfPublished = true
                };
                RetrieveAttributeResponse raResponse = (RetrieveAttributeResponse)service.Execute(raRequest);
                PicklistAttributeMetadata paMetadata = (PicklistAttributeMetadata)raResponse.AttributeMetadata;
                OptionMetadata[] optionList = paMetadata.OptionSet.Options.ToArray();
                foreach (OptionMetadata oMD in optionList)
                {
                    //int value = Int.TryParse(optionSetvalue);
                    if (oMD.Value == optionSetvalue)
                    {
                        string outPutValue = "";
                        if (lcid == 0)
                        {
                            log.LogInfo($"field Option language lcid both ");
                            outPutValue = oMD.Label.LocalizedLabels.Where(x => x.LanguageCode == 1033).FirstOrDefault().Label.ToString();
                            log.LogInfo($"English  {outPutValue}");
                            log.LogInfo($"English  {oMD.Label.LocalizedLabels.Where(x => x.LanguageCode == 1033).FirstOrDefault().Label.ToString()}");
                            log.LogInfo($"Arabic { oMD.Label.LocalizedLabels.Where(x => x.LanguageCode == 1025).FirstOrDefault().Label.ToString()}");
                            //  + " - " + oMD.Label.LocalizedLabels.Where(x => x.LanguageCode == 1025).FirstOrDefault().Label.ToString();
                            return outPutValue;
                        }
                        else
                        {
                            return  oMD.Label.LocalizedLabels.Where(x => x.LanguageCode == lcid).FirstOrDefault().Label.ToString();
                        }
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
                // throw new
            }
        }

        private string GetFieldValueToString(Entity entity, string fieldName)
        {
            if (entity.Attributes.Contains(fieldName))
            {
                if (entity.Attributes[fieldName] is String) //String
                    return ((String)entity.Attributes[fieldName]).ToString();

                else if (entity.Attributes[fieldName] is OptionSetValue) //OptionSet
                {
                    var desicion2 = entity.GetAttributeValue<OptionSetValue>(fieldName).Value;
                    int fieldOptionNumber = desicion2;
                    var ArabicValue = LocalOptionSetList(entity.LogicalName, fieldName, fieldOptionNumber, 1025);
                    var EnglishValue = LocalOptionSetList(entity.LogicalName, fieldName, fieldOptionNumber, 1033);

                    string text = entity.FormattedValues[fieldName].ToString();
                    return ArabicValue;
                    //return ((OptionSetValue)entity.Attributes[fieldName]).Value.ToString();
                }

                else if (entity.Attributes[fieldName] is DateTime) //DateTime
                    return ((DateTime)entity.Attributes[fieldName]).ToLocalTime().ToString();

                else if (entity.Attributes[fieldName] is BooleanManagedProperty) //Boolean
                    return ((BooleanManagedProperty)entity.Attributes[fieldName]).Value.ToString();

                else if (entity.Attributes[fieldName] is float) //float
                    return ((float)entity.Attributes[fieldName]).ToString();

                else if (entity.Attributes[fieldName] is int) //Integer
                    return ((int)entity.Attributes[fieldName]).ToString();

                else if (entity.Attributes[fieldName] is EntityReference) //Lookup
                    return ((EntityReference)entity.Attributes[fieldName]).Name;

                else if (entity.Attributes[fieldName] is Money) //Crm Money
                    return ((Money)entity.Attributes[fieldName]).Value.ToString();

                else if (entity.Attributes[fieldName] is decimal) //Decimal
                    return ((decimal)entity.Attributes[fieldName]).ToString();

                else if (entity.Attributes[fieldName] is Guid) //Guid
                    return (entity.Attributes[fieldName].ToString());

                else
                    return "";
            }
            else
                return "";
        }

    }
}

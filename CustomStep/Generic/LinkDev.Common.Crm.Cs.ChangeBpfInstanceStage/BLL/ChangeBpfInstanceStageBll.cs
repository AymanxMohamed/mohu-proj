
using LinkDev.Common.Crm.Logger;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Bll.ChangeBpfInstanceStage
{
   public class ChangeBpfInstanceStageBll : LinkDev.Common.Crm.Bll.Base.BllBase
    {
        public ChangeBpfInstanceStageBll(IOrganizationService organizationService,ILogger logger, string languageCode )
            :base(organizationService,logger,languageCode)
        { }
        #region Logic Functions
        public void ChangeBPFProcessStage(Guid recordId, string recordEntityLogicalName, bool moveToNextStage, bool backToPreviousStage, bool moveToSpecificStage, EntityReference processStage)
        {
            try
            {
                Entity ProcessInstanceEntity = RetrieveProcessInstance(recordId, recordEntityLogicalName);
                DataCollection<Entity> StagesEntities = RetrieveActivePathStagesEntites(ProcessInstanceEntity.Id);
                Guid BpfProcessID = ((EntityReference)ProcessInstanceEntity["processid"]).Id;
                if (BpfProcessID != Guid.Empty)
                {
                 string ProcessEntityLogicalName = RetrieveProcessInstanceEntityLogicalName(BpfProcessID);
                    Guid ActiveStageId = new Guid(ProcessInstanceEntity.Attributes["processstageid"].ToString());
                    
                    int ActiveStagePosition = GetActiveStagePosition(StagesEntities, ActiveStageId);
                    if(moveToNextStage == true)
                    {
                        MoveToNextStage(ActiveStagePosition, StagesEntities, ProcessEntityLogicalName, ProcessInstanceEntity.Id);
                    }
                    else if(backToPreviousStage == true)
                    {
                        BackToPervious(ActiveStagePosition, StagesEntities, ProcessEntityLogicalName, ProcessInstanceEntity.Id);
                    }
                    else if(moveToSpecificStage == true)
                    {
                        MoveToSpecificStage(processStage.Id, StagesEntities, ProcessEntityLogicalName, ProcessInstanceEntity.Id);
                    }

                 }
                else
                {
                    throw new InvalidPluginExecutionException("BPF ID Equal Null");
                }

            }
            catch(Exception ex)
            {
                throw new InvalidPluginExecutionException("Function: ExcuteLogicBll:" + ex.Message);
            }
        }
        public Entity RetrieveProcessInstance(Guid recordId,string recordEntityLogicalName)
        {
            try
            {
                if(recordId != Guid.Empty && recordEntityLogicalName != string.Empty && recordEntityLogicalName != null)
                {
                    RetrieveProcessInstancesRequest RetrieveProcessInstanceRequest = new RetrieveProcessInstancesRequest
                    {
                        EntityId = recordId,
                        EntityLogicalName = recordEntityLogicalName
                    };
                    RetrieveProcessInstancesResponse RetrieveProcessInstanceResponse = (RetrieveProcessInstancesResponse)OrganizationService.Execute(RetrieveProcessInstanceRequest);
                    if (RetrieveProcessInstanceResponse.Processes.Entities != null )
                    {
                        Entity activeProcessInstanceEntity = RetrieveProcessInstanceResponse.Processes.Entities[0];

                        return activeProcessInstanceEntity;
                    }
                    else
                    {
                        throw new InvalidPluginExecutionException("Active Process Instance Entity Equal Null");
                    }
                }
                else
                {
                    throw new InvalidPluginExecutionException("record id or logical name equal null");
                }
            }
            catch(Exception ex)
            {
                throw new InvalidPluginExecutionException("Function: RetrieveProcessInstance:" + ex.Message);
            }
        }
        public DataCollection<Entity> RetrieveActivePathStagesEntites(Guid activeProcessInstanceId)
        {
            try
            {
                if(activeProcessInstanceId != Guid.Empty)
                {
                    RetrieveActivePathRequest RetrieveActivePathRequest = new RetrieveActivePathRequest
                    {
                        ProcessInstanceId = activeProcessInstanceId
                    };

                    RetrieveActivePathResponse RetrieveActivePathResponse = (RetrieveActivePathResponse)OrganizationService.Execute(RetrieveActivePathRequest);

                    if(RetrieveActivePathResponse.ProcessStages.Entities != null)
                    {
                        DataCollection<Entity> StagesEntities = RetrieveActivePathResponse.ProcessStages.Entities;
                        return StagesEntities;
                    }
                    else
                    {
                        throw new InvalidPluginExecutionException("Process Stages equal null");
                    }

                }
                else
                {
                    throw new InvalidPluginExecutionException("active Process Instance Id equal null");
                }

            }
            catch(Exception ex)
            {
                throw new InvalidPluginExecutionException("Function: RetrieveActivePathStagesEntites:" + ex.Message);
            }
        }
        public String RetrieveProcessInstanceEntityLogicalName (Guid businessProcessId)
        {
            try
            {
                if (businessProcessId != Guid.Empty)
                {
                    
                    Entity processEntity = OrganizationService.Retrieve("workflow", businessProcessId, new ColumnSet("uniquename"));
                    if (processEntity.Contains("uniquename") && processEntity["uniquename"] != null)
                    {
                        string ProcInstanceLogicalName = processEntity["uniquename"].ToString();
                        return ProcInstanceLogicalName;
                    }
                    else
                    {
                        throw new InvalidPluginExecutionException("Process Intance Logical Name Equal Null");
                    }

                }
                else
                {
                    throw new InvalidPluginExecutionException("Business Process ID equal null");
                }

            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("Function: RetrieveProcessInstanceEntityLogicalName:" + ex.Message);
            }
        }
        private int GetActiveStagePosition (DataCollection<Entity> stagesEntites,Guid activeStageId)
        {
            try
            {
              
                int position = -1;
                if (activeStageId != null)
                {
                    for (int i = 0; i < stagesEntites.Count; i++ )
                    {
                      
                        if (stagesEntites[i].Attributes["processstageid"].ToString() == activeStageId.ToString())
                        {
                            
                            position = i;
                        }
                    }

                    if(position != -1 )
                    {
                        return position;
                    }
                    else
                    {
                        throw new InvalidPluginExecutionException("Active Stage Not found in Active Path");
                    }
                }
                else
                {
                    throw new InvalidPluginExecutionException("active stage id equal null");
                }
            }
            catch(Exception ex)
            {
                throw new InvalidPluginExecutionException("Function: GetActiveStagePosition:" + ex.Message);
            }
        }
        private void MoveToNextStage(int position, DataCollection<Entity> stagesEntites,string processInstanceEntityLogicalName,Guid activeProcessInstanceId)
        {
            try
            {
                if (stagesEntites != null)
                {
                    if (position >= 0 && (position + 1) <= stagesEntites.Count)
                    {
                        if (processInstanceEntityLogicalName != null)
                        {
                            if (activeProcessInstanceId != Guid.Empty)
                            {
                                Guid nextstage = (Guid)stagesEntites[position + 1].Attributes["processstageid"];
                                Entity ProcessInstanceEntity = new Entity(processInstanceEntityLogicalName);
                                ProcessInstanceEntity.Id = activeProcessInstanceId;
                                ProcessInstanceEntity["activestageid"] = new EntityReference(stagesEntites[position + 1].LogicalName, nextstage);
                                OrganizationService.Update(ProcessInstanceEntity);
                            }
                            else
                            {
                                throw new InvalidPluginExecutionException("Process Instance Entity Id Equal Null");
                            }
                        }
                        else
                        {
                            throw new InvalidPluginExecutionException("Process Instance Entity Logical Name Equal Null");
                        }
                    }
                    else
                    {
                        throw new InvalidPluginExecutionException("Position of the next stage is not correct");
                    }
                }
                else
                {
                    throw new InvalidPluginExecutionException("stages entites equal null");
                }
            }
            catch(Exception ex)
            {
                throw new InvalidPluginExecutionException("Function: MoveToNextStage:" + ex.Message);
            }
        }
        private void BackToPervious(int position, DataCollection<Entity> stagesEntites, string processInstanceEntityLogicalName, Guid activeProcessInstanceId)
        {
            try
            {
                if (stagesEntites != null)
                {
                    if (position > 0 )
                    {
                        if (processInstanceEntityLogicalName != null)
                        {
                            if (activeProcessInstanceId != Guid.Empty)
                            {
                                Guid backstage = (Guid)stagesEntites[position -1 ].Attributes["processstageid"];
                                Entity ProcessInstanceEntity = new Entity(processInstanceEntityLogicalName);
                                ProcessInstanceEntity.Id = activeProcessInstanceId;
                                ProcessInstanceEntity["activestageid"] = new EntityReference(stagesEntites[position - 1].LogicalName, backstage);
                                OrganizationService.Update(ProcessInstanceEntity);
                            }
                            else
                            {
                                throw new InvalidPluginExecutionException("Process Instance Entity Id Equal Null");
                            }
                        }
                        else
                        {
                            throw new InvalidPluginExecutionException("Process Instance Entity Logical Name Equal Null");
                        }
                    }
                    else
                    {
                        throw new InvalidPluginExecutionException("Position of the back stage is not correct");
                    }
                }
                else
                {
                    throw new InvalidPluginExecutionException("stages entites equal null");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("Function: MoveToNextStage:" + ex.Message);
            }
        }
        private void MoveToSpecificStage(Guid StageId, DataCollection<Entity> stagesEntites, string processInstanceEntityLogicalName,Guid activeProcessInstanceId)
        {
            try
            {
                if (stagesEntites != null)
                {
                  if(StageId !=null)
                    {
                        bool CheckStage = false;
                        for(int i=0;i<stagesEntites.Count;i++)
                        {
                            if (stagesEntites[i].Attributes["processstageid"].ToString() == StageId.ToString())
                            {
                                CheckStage = true;
                            }
                        }
                        if(CheckStage == true)
                        {

                            if (processInstanceEntityLogicalName != null)
                            {
                                if (activeProcessInstanceId != Guid.Empty)
                                {

                                    #region clear traversed Path
                                    //Entity ProcessInstanceEntityForUpdateTravesed = new Entity(processInstanceEntityLogicalName);
                                    //ProcessInstanceEntityForUpdateTravesed.Id = activeProcessInstanceId;
                                    //ProcessInstanceEntityForUpdateTravesed["traversedpath"] = string.Empty;
                                    //OrganizationService.Update(ProcessInstanceEntityForUpdateTravesed);
                                    #endregion
                                    #region Update Process Instance with stage
                                    Entity ProcessInstanceEntityForUpdateStage = new Entity(processInstanceEntityLogicalName);
                                    ProcessInstanceEntityForUpdateStage.Id = activeProcessInstanceId;
                                    ProcessInstanceEntityForUpdateStage["activestageid"] = new EntityReference("processstage", StageId);
                                    OrganizationService.Update(ProcessInstanceEntityForUpdateStage);
                                    #endregion
                                }
                                else
                                {
                                    throw new InvalidPluginExecutionException("Process Instance Entity Id Equal Null");
                                }
                            }
                            else
                            {
                                throw new InvalidPluginExecutionException("Process Instance Entity Logical Name Equal Null");
                            }
                        }
                        else
                        {

                        }
                    }
                  else
                    {
                        throw new InvalidPluginExecutionException("stage id equal null");
                    }
                }
                else
                {
                    throw new InvalidPluginExecutionException("stages entites equal null");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("Function: MoveToSpecificStage:" + ex.Message);
            }
        }

        private DataCollection<Entity> RetrieveActivePathStagesEntitesToAnotherProcess(Guid activeProcessInstanceId)
        {
            try
            {
                if (activeProcessInstanceId != Guid.Empty)
                {
                    RetrieveActivePathRequest RetrieveActivePathRequest = new RetrieveActivePathRequest
                    {
                        ProcessInstanceId = activeProcessInstanceId,

                    };

                    RetrieveActivePathResponse RetrieveActivePathResponse = (RetrieveActivePathResponse)OrganizationService.Execute(RetrieveActivePathRequest);

                    if (RetrieveActivePathResponse.ProcessStages.Entities != null)
                    {
                        DataCollection<Entity> StagesEntities = RetrieveActivePathResponse.ProcessStages.Entities;
                        return StagesEntities;
                    }
                    else
                    {
                        throw new InvalidPluginExecutionException("Process Stages equal null");
                    }

                }
                else
                {
                    throw new InvalidPluginExecutionException("active Process Instance Id equal null");
                }

            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("Function: RetrieveActivePathStagesEntites:" + ex.Message);
            }
        }
        private void MoveToSpecificProcessAndStage(Guid StageId, DataCollection<Entity> stagesEntites, string processInstanceEntityLogicalName, Guid activeProcessInstanceId)
        {
            try
            {
                if (stagesEntites != null)
                {
                    if (StageId != null)
                    {
                        bool CheckStage = false;
                        for (int i = 0; i < stagesEntites.Count; i++)
                        {
                            if (stagesEntites[i].Attributes["processstageid"].ToString() == StageId.ToString())
                            {
                                CheckStage = true;
                            }
                        }
                        if (CheckStage == true)
                        {

                            if (processInstanceEntityLogicalName != null)
                            {
                                if (activeProcessInstanceId != Guid.Empty)
                                {

                                    #region clear traversed Path
                                    Entity ProcessInstanceEntityForUpdateTravesed = new Entity(processInstanceEntityLogicalName);
                                    ProcessInstanceEntityForUpdateTravesed.Id = activeProcessInstanceId;
                                    ProcessInstanceEntityForUpdateTravesed["traversedpath"] = string.Empty;
                                    OrganizationService.Update(ProcessInstanceEntityForUpdateTravesed);
                                    #endregion
                                    #region Update Process Instance with stage
                                    Entity ProcessInstanceEntityForUpdateStage = new Entity(processInstanceEntityLogicalName);
                                    ProcessInstanceEntityForUpdateStage.Id = activeProcessInstanceId;
                                    ProcessInstanceEntityForUpdateStage["activestageid"] = new EntityReference("processstage", StageId);
                                    ProcessInstanceEntityForUpdateStage["processid"] = activeProcessInstanceId;

                                    OrganizationService.Update(ProcessInstanceEntityForUpdateStage);
                                    #endregion
                                }
                                else
                                {
                                    throw new InvalidPluginExecutionException("Process Instance Entity Id Equal Null");
                                }
                            }
                            else
                            {
                                throw new InvalidPluginExecutionException("Process Instance Entity Logical Name Equal Null");
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        throw new InvalidPluginExecutionException("stage id equal null");
                    }
                }
                else
                {
                    throw new InvalidPluginExecutionException("stages entites equal null");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("Function: MoveToSpecificStage:" + ex.Message);
            }
        }

        public void ChangeBPFAndProcessStage(string InstanceLogicalName, Guid entityId, string entityLogicalName, EntityReference targetProcess, EntityReference targetActiveStage , ITracingService tracingService)
        {
            if (entityLogicalName != null && entityLogicalName != string.Empty &&
                entityId != null && entityId != Guid.Empty &&
                targetProcess != null && targetProcess?.Id != Guid.Empty &&
                targetActiveStage != null && targetActiveStage?.Id != Guid.Empty)
            {
                tracingService.Trace($"in ChangeBPFAndProcessStage method");

                #region Query to Retrieve Instance id
                var requestInstanceQuerey = new QueryExpression(InstanceLogicalName);
                requestInstanceQuerey.Distinct = true;
                //requestInstanceQuerey.ColumnSet.AddColumns( "activestageid", "processid", "traversedpath");
                requestInstanceQuerey.Criteria.AddCondition("bpf_" + entityLogicalName + "id", ConditionOperator.Equal, entityId);
                requestInstanceQuerey.Criteria.AddCondition("processid"  , ConditionOperator.Equal, targetProcess.Id);
                #endregion

                EntityCollection retrieved = OrganizationService.RetrieveMultiple(requestInstanceQuerey);

                if (!retrieved.Entities.Any() || retrieved.Entities[0] == null || retrieved.Entities[0]?.Id == null)
                {
                    // throw new InvalidPluginExecutionException(String.Format($"Process '{targetProcess.Id}' not found"));
                    tracingService.Trace($"Process '{targetProcess.Id}' not found");
                }
                else
                {
                    tracingService.Trace($"instance logical anme '{retrieved.Entities[0].LogicalName}', Id : '{ retrieved.Entities[0].Id}'");
                    //// Change the stage
                    Entity instanceEntity = new Entity(retrieved.Entities[0].LogicalName, retrieved.Entities[0].Id);  
                    instanceEntity.Attributes.Add("activestageid", targetActiveStage);
                    OrganizationService.Update(instanceEntity);
                    tracingService.Trace($"updated Process '{targetProcess.Id}' ");
                }
            }
        }      
        #endregion
    }
}

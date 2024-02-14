using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;

using Microsoft.Xrm.Sdk.Query;
using LinkDev.Common.Crm.Plugin.Base;
using LinkDev.Common.Crm.Logger;
using SeverityLevel = LinkDev.Common.Crm.Logger.SeverityLevel;
using LinkDev.Common.Crm.Bll.ChangeBpfInstanceStage;

namespace LinkDev.Common.CRM.Plugins.BPFInstance
{
    public class PostUpdateBPFInstanceActiveStage : PluginBase

    {
        private readonly string UnsecureConfiguration;

        public PostUpdateBPFInstanceActiveStage(string unsecureString)
        {
            if (String.IsNullOrWhiteSpace(unsecureString))
            {
                throw new InvalidPluginExecutionException("Unsecure string are required for this plugin to execute.");
            }
            UnsecureConfiguration = unsecureString;
        }
        public override void ExtendedExecute()
        {
            Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"in ExtendedExecute ", SeverityLevel.Info);

            if (Context.InputParameters.Contains("Target") && Context.InputParameters["Target"] is Entity)
            {
                Entity targetEntity = Context.InputParameters["Target"] as Entity;

                Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"  targetEntity logical name{targetEntity.LogicalName} ", SeverityLevel.Info);
                if (Context.MessageName.Contains("Update"))
                {
                    if (Context.PreEntityImages != null)
                    {
                        var splitUnsecureString = UnsecureConfiguration.Split(';');
                        string EntityLookupName = splitUnsecureString[1];
                        Tracer.LogComment(LoggerHandler.GetMethodFullName(), $" contain PreEntityImages ", SeverityLevel.Info);
                        if (targetEntity.Attributes.Contains("activestageid") && Context.PreEntityImages["PreImage"].Attributes.Contains("activestageid") && Context.PreEntityImages["PreImage"].Attributes.Contains(EntityLookupName))
                        {
                            Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"PreEntityImage Logical Name '{Context.PreEntityImages["PreImage"].LogicalName}'", SeverityLevel.Info);
                            if (((EntityReference)targetEntity.Attributes["activestageid"]).Id != ((EntityReference)Context.PreEntityImages["PreImage"].Attributes["activestageid"]).Id)
                            {
                                String[] workflowsid = splitUnsecureString[0].Split(',');
                                Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"Number of workflows Id found in UnsecureConfiguration '{workflowsid.Length}'", SeverityLevel.Info);
                                EntityReference entityReference = (EntityReference)Context.PreEntityImages["PreImage"].Attributes[EntityLookupName];
                                Entity entity = OrganizationService.Retrieve(entityReference.LogicalName, entityReference.Id, new ColumnSet("ldv_specifiedstageid"));
                                if (entity.Contains("ldv_specifiedstageid") && entity["ldv_specifiedstageid"] != null &&
                                     entity.GetAttributeValue<EntityReference>("ldv_specifiedstageid").Id != ((EntityReference)targetEntity.Attributes["activestageid"]).Id)
                                {
                                    Entity entitytoupdate = new Entity(entityReference.LogicalName, entityReference.Id);
                                    entitytoupdate["ldv_specifiedstageid"] = null;
                                    OrganizationService.Update(entitytoupdate);

                                    ChangeBpfInstanceStageBll changeBpfInstanceStageBll = new ChangeBpfInstanceStageBll(OrganizationService, Tracer, LanguageCode);
                                    changeBpfInstanceStageBll.ChangeBPFProcessStage(entityReference.Id, entityReference.LogicalName, false, false, true, entity.GetAttributeValue<EntityReference>("ldv_specifiedstageid"));
                                }
                                else
                                {
                                    Tracer.LogComment(LoggerHandler.GetMethodFullName(), ((EntityReference)targetEntity.Attributes["activestageid"]).Id.ToString(), SeverityLevel.Warning);
                                    if (workflowsid != null && workflowsid.Count() > 0)
                                    {
                                        foreach (string workflowid in workflowsid)
                                        {
                                            Tracer.LogComment(LoggerHandler.GetMethodFullName(), "workflow id:" + Guid.Parse(workflowid), SeverityLevel.Info);
                                            Tracer.LogComment(LoggerHandler.GetMethodFullName(), "target entity:" + targetEntity.Id, SeverityLevel.Info);
                                            ExecuteWorkflowRequest request = new ExecuteWorkflowRequest()
                                            {
                                                WorkflowId = Guid.Parse(workflowid),
                                                EntityId = targetEntity.Id,
                                            };
                                            //Tracer.LogComment(LoggerHandler.GetMethodFullName(), "request.RequestId:" + request.RequestId, SeverityLevel.Info);                                      
                                            // Execute the workflow.
                                            ExecuteWorkflowResponse response =
                                            (ExecuteWorkflowResponse)OrganizationService.Execute(request);
                                            //Tracer.LogComment(LoggerHandler.GetMethodFullName(), "response id:" + response.Id, SeverityLevel.Info);
                                        }
                                    }
                                    else
                                    {
                                        Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"workflowsid Number  =0 or null  ", SeverityLevel.Info);
                                    }
                                }
                            }
                            else
                            {
                                Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"PreImage == activestageid", SeverityLevel.Info);
                            }
                        }
                        else
                        {
                            Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"PreImage is null or  activestageid is null or " + EntityLookupName + " is null", SeverityLevel.Info);
                        }
                    }
                    else
                    {
                        Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"Context.PreEntityImages is null  ", SeverityLevel.Info);
                    }
                }
                else
                {
                    Tracer.LogComment(LoggerHandler.GetMethodFullName(), $"Context not update   ", SeverityLevel.Info);
                }
            }
            else
            {
                throw new InvalidPluginExecutionException("No Target Found");
            }
        }
    }
}


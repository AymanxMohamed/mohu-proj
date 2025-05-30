﻿// <copyright file="WorkFlowActivityBase.cs" company="">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author></author>
// <date>11/28/2017 6:44:21 PM</date>
// <summary>Implements the WorkFlowActivityBase Workflow Activity.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Maan.Core.Helper
{
    public abstract class WorkFlowActivityBase : CodeActivity
    {
        public sealed class LocalWorkflowContext
        {
            public IServiceProvider ServiceProvider
            {
                get;

                private set;
            }

            public IOrganizationService OrganizationService
            {
                get;

                private set;
            }

            public IWorkflowContext WorkflowExecutionContext
            {
                get;

                private set;
            }

            public ITracingService TracingService
            {
                get;

                private set;
            }

            private LocalWorkflowContext()
            {
            }

            internal LocalWorkflowContext(CodeActivityContext executionContext)
            {
                if (executionContext == null)
                {
                    throw new ArgumentNullException("serviceProvider");
                }

                // Obtain the execution context service from the service provider.
                this.WorkflowExecutionContext = (IWorkflowContext)executionContext.GetExtension<IWorkflowContext>();

                // Obtain the tracing service from the service provider.
                this.TracingService = (ITracingService)executionContext.GetExtension<ITracingService>();

                // Obtain the Organization Service factory service from the service provider
                IOrganizationServiceFactory factory = (IOrganizationServiceFactory)executionContext.GetExtension<IOrganizationServiceFactory>();

                // Use the factory to generate the Organization Service.
                this.OrganizationService = factory.CreateOrganizationService(this.WorkflowExecutionContext.UserId);
            }

            internal void Trace(string message)
            {
                if (string.IsNullOrWhiteSpace(message) || this.TracingService == null)
                {
                    return;
                }

                if (this.WorkflowExecutionContext == null)
                {
                    this.TracingService.Trace(message);
                }
                else
                {
                    this.TracingService.Trace(
                        "{0}, Correlation Id: {1}, Initiating User: {2}",
                        message,
                        this.WorkflowExecutionContext.CorrelationId,
                        this.WorkflowExecutionContext.InitiatingUserId);
                }
            }
        }

        protected override void Execute(CodeActivityContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }

            // Construct the Local plug-in context.
            LocalWorkflowContext localcontext = new LocalWorkflowContext(context);

            //localcontext.Trace(string.Format(CultureInfo.InvariantCulture, "Entered {0}.Execute()", this.ChildClassName));

            try
            {
                //// Iterate over all of the expected registered events to ensure that the plugin
                //// has been invoked by an expected event
                //// For any given plug-in event at an instance in time, we would expect at most 1 result to match.
                //Action<LocalWorkflowContext> entityAction =
                //    (from a in this.RegisteredEvents
                //     where (
                //     a.Item1 == localcontext.PluginExecutionContext.Stage &&
                //     a.Item2 == localcontext.PluginExecutionContext.MessageName &&
                //     (string.IsNullOrWhiteSpace(a.Item3) ? true : a.Item3 == localcontext.PluginExecutionContext.PrimaryEntityName)
                //     )
                //     select a.Item4).FirstOrDefault();

                //if (entityAction != null)
                //{
                //    localcontext.Trace(string.Format(
                //        CultureInfo.InvariantCulture,
                //        "{0} is firing for Entity: {1}, Message: {2}",
                //        this.ChildClassName,
                //        localcontext.PluginExecutionContext.PrimaryEntityName,
                //        localcontext.PluginExecutionContext.MessageName));

                //    entityAction.Invoke(localcontext);

                //    // now exit - if the derived plug-in has incorrectly registered overlapping event registrations,
                //    // guard against multiple executions.
                //    return;
                //}
                ExecuteCRMWorkFlowActivity(context, localcontext);

            }
            catch (FaultException<OrganizationServiceFault> e)
            {
                localcontext.Trace(string.Format(CultureInfo.InvariantCulture, "Exception: {0}", e.ToString()));

                // Handle the exception.
                throw;
            }
            finally
            {
                //localcontext.Trace(string.Format(CultureInfo.InvariantCulture, "Exiting {0}.Execute()", this.ChildClassName));
            }
        }

        public virtual void ExecuteCRMWorkFlowActivity(CodeActivityContext context, LocalWorkflowContext crmWorkflowContext)
        {
            // Do nothing. 
        }


    }
}

using LinDev.MOHU.Utilites.Model;
using LinkDev.MAAN.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinDev.MOHU.Utilites.Logic
{
    public class CreateBPFInstanceLogic  : StepLogic< CreateBPFInstance>
    {
        protected override void ExecuteLogic()
        {
            if (codeActivity.Service.Get(executionContext)?.Id !=Guid.Empty)
            {
                EntityReference service = codeActivity.Service.Get(executionContext);
                EntityReference bpfProcess = GetBPFFromService(service);


            }


        }
        EntityReference GetBPFFromService(EntityReference service)
        {
            EntityReference BPF = null;

            return BPF;
        }

        void UpdateRequestService(EntityReference bpfProcess, EntityReference request )
        {
            Entity requestEntity = new Entity(request.LogicalName, request.Id);
            requestEntity.Attributes.Add(RequestEntity.ProcessId, bpfProcess.Id) ;
        }
    }
}

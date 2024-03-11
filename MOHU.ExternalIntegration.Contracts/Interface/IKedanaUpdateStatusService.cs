using MOHU.ExternalIntegration.Contracts.Dto.Kedana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Interface
{
    public interface IKedanaUpdateStatusService
    {

        Task<bool> KedanaUpdateStatus(KedanaUpdateStatusResponse model);
    }
}

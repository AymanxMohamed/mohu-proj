using MOHU.Integration.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IUpdateStatusService
    {
        //general service 
        Task<bool> UpdateStatus(UpdateStatusRequest model);

    }
}

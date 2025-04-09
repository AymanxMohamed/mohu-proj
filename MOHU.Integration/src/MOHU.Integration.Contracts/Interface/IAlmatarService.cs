using MOHU.Integration.Contracts.Dto.Almatar;
using MOHU.Integration.Contracts.Dto.Sahab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IAlmatarService
    {
        Task<bool> UpdateStatusAsync(AlmatarUpdateStatusRequest request);

    }
}

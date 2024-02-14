using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Utilities
{
    public interface IRetryAPICall
    {
        bool IsAllowRetry(Exception exception);
       
    }
}

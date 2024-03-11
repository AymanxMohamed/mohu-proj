using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Interface.Cache
{
    public interface ICacheService<T>
    {
        Task<T> GetAsync(string key);
        Task SetAsync(string key, T value);
        Task Clear();
        Task Remove(string key);
    }
}

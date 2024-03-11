using MOHU.ExternalIntegration.Contracts.Dto.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Interface
{
    public interface IFieldService
    {
        Task<IEnumerable<FieldDto>> GetFieldsBySubCategoryAsync(string subCategoryId);
    }
}

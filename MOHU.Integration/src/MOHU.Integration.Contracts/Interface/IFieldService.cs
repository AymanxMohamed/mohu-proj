using MOHU.Integration.Contracts.Dto.Field;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IFieldService
    {
        Task<IEnumerable<FieldDto>> GetFieldsBySubCategoryAsync(string subCategoryId);
    }
}

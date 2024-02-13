using MOHU.Integration.Contracts.Dto.Common;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IMessageService
    {
        Task<MessageDto> GetMessageByCodeAsync(string code);
    }
}

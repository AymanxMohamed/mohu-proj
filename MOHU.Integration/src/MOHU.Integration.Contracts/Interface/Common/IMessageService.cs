using MOHU.Integration.Contracts.Dto.Common;

namespace MOHU.Integration.Contracts.Interface.Common
{
    public interface IMessageService
    {
        Task<MessageDto> GetMessageByCodeAsync(string code);
    }
}

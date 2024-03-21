using Microsoft.Extensions.Localization;

namespace MOHU.Integration.Application.Service
{
    public class BaseService
    {
        private readonly IStringLocalizer _stringLocalizer;

        public BaseService(IStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public void ThrowBadRequestException(string errorCode)
        {
            
        }
        public void ThrowNotFoundException(string errorCode)
        {

        }
        public void ThrowUnauthorizedException(string errorCode) 
        {
            
        }
    }
}

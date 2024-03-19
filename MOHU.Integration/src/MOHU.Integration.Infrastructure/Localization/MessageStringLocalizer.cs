using Microsoft.Extensions.Localization;
using MOHU.Integration.Contracts.Interface.Cache;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Shared;
using System.Globalization;

namespace MOHU.Integration.Infrastructure.Localization
{
    public class MessageStringLocalizer : IStringLocalizer
    {
        private readonly IMessageService _messageService;
        private readonly ICacheService _cacheService;
        public MessageStringLocalizer(IMessageService messageService, ICacheService cacheService)
        {
            _messageService = messageService;
            _cacheService = cacheService;
        }
        public LocalizedString this[string name]
        {
            get
            {
                var languageKey = LanguageHelper.IsArabic ? "ar" : "en";
                var cacheKey = $"Msg-{name}_{languageKey}";
                var message = _cacheService.GetAsync<string>(cacheKey).Result;
                if(message is null)
                {
                     var messageDto = _messageService.GetMessageByCodeAsync(name).Result;
                    if(messageDto is null)
                        return new LocalizedString(name, name, true);
                    message = messageDto.ErrorMessage;
                    _cacheService.SetAsync(cacheKey, message).Wait();

                }
                return new LocalizedString(name,message);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return default;
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }



    }
}

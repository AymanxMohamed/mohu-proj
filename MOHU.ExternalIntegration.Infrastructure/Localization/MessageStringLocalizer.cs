using Microsoft.Extensions.Localization;
using MOHU.Integration.Contracts.Interface.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Infrastructure.Localization
{
    public class MessageStringLocalizer : IStringLocalizer
    {
        private readonly IMessageService _messageService;
        public MessageStringLocalizer(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public LocalizedString this[string name]
        {
            get
            {
                var message = _messageService.GetMessageByCodeAsync(name).Result;
                if (message is null)
                    return new LocalizedString(name, name, true);
                return new LocalizedString(name, message.ErrorMessage);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            // Implement logic to retrieve localized strings
            return default;
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            // Implement logic to switch to a different culture
            throw new NotImplementedException();
        }



    }
}

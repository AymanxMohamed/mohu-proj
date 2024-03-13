using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Enum
{
    public enum PreferredLanguageEnum
    {

        [Display(Name = "Arabic")]
        Arabic = 1,

        [Display(Name = "English")]
        English = 2,
    }
}

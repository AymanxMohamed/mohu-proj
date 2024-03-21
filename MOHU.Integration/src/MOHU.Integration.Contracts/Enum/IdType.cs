using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Enum
{
    public enum IdType
    {
        [Display(Name = "National Identity")]
        NationalIdentity = 1,
        [Display(Name = "Accommodation")]
        Accommodation = 2,
        [Display(Name = "Gulf citizen")]
        Gulfcitizen = 3,
        [Display(Name = "Passport (in the case of a non-citizen or resident)")]
        Passport = 4,
    }
}

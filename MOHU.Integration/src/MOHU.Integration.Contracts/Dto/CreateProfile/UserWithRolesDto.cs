using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.CreateProfile
{
    public class UserWithRolesDto
    {
        public Guid SystemUserId { get; set; }
        public string FullName { get; set; }
        public string PrimaryEmail { get; set; }

        public HashSet<string> AllRoles { get; set; } // Using HashSet to prevent duplicates;
    }
}

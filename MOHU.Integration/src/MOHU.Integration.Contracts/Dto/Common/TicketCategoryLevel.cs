using MOHU.Integration.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Contracts.Dto.Common;
public class TicketCategoryLevel
{
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public CategoryLevelsEnum CategoryLevel { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Dto.Ticket
{
    public class CancelTicketResponse
    {

        public Guid TicketId { get; set; }



        [IgnoreDataMember]
        [EnumDataType(typeof(CancelStatusEnum))]
        public int Cancel { get; private set; } = (int)CancelStatusEnum.Canceled;


    }

    public enum CancelStatusEnum
    {
        [Display(Name = "Canceled")]
        Canceled = 2,
    }
}

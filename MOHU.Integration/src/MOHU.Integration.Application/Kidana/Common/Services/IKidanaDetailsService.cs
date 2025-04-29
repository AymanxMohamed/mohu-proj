using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Services
{
    public interface IKidanaDetailsService
    {
         Task<ErrorOr<TicketValidationResult>> ValidateTicketWithCrmCheck(string ticketId);
    }
}

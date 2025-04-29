using Core.Application.Integrations.Clients;
using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Clients
{
    public interface IKidanaClient : IRestClientService
    {
        ErrorOr<KidanaResponseBase<KidanaDetailsResponse>> ValidateTicket(string kidanaNumber);
    }
}

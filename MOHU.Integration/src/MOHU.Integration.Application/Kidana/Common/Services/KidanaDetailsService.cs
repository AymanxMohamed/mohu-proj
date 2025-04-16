using MOHU.Integration.Application.Kidana.Common.Clients;
using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Services
{
    public class KidanaDetailsService(IKidanaClient client) : IKidanaDetailsService
    {
        public ErrorOr<KidanaDetailsResponse> GetDetails(string kidanaNumber) =>
            client.GetDetails(kidanaNumber);
    }
}

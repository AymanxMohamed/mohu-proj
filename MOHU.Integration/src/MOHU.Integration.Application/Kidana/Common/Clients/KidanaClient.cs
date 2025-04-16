using Core.Infrastructure.Integrations.Clients;
using Core.Infrastructure.Integrations.Clients.Settings;
using Microsoft.Extensions.Logging;
using MOHU.Integration.Application.Kidana.Common.Dtos.Requests;
using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Clients
{


    internal class KidanaClient(
     KidanaApiSettings settings,
     ILogger<RestClientService> logger) : RestClientService(settings, logger), IKidanaClient
    {
        public ErrorOr<KidanaDetailsResponse> GetDetails(string kidanaNumber)
        {
            if (settings.UseFileClients)
                return LoadTestData();

            var request = new RestRequest("kidana/details")
                .AddQueryParameter("kidanaNumber", kidanaNumber);

            return ExecuteRequest<KidanaResponseBase<KidanaDetailsResponse>>(request)
                .Match(
                    response => response.EnsureSuccess(),
                    error => error
                );
        }

        private ErrorOr<KidanaDetailsResponse> LoadTestData()
        {
            try
            {
                var json = File.ReadAllText(settings.TestDataPath);
                var response = JsonConvert.DeserializeObject<KidanaResponseBase<KidanaDetailsResponse>>(json);
                return response?.EnsureSuccess() ?? Error.NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load test data");
                return Error.Unexpected("TEST_DATA_ERROR", ex.Message);
            }
        }
    }
}

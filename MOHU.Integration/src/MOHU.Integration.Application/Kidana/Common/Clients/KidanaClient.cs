using Common.Crm.Infrastructure.Repositories.Interfaces;
using Core.Domain.Integrations.Clients;
using Core.Infrastructure.Integrations.Clients;
using Core.Infrastructure.Integrations.Clients.Settings;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Logging;
using MOHU.Integration.Application.Kidana.Common.Dtos.Requests;
using MOHU.Integration.Application.Kidana.Common.Dtos.Responses;
using MOHU.Integration.Application.Kidana.Common.Services;
using MOHU.Integration.Domain.Features.Tickets;
using MOHU.Integration.Infrastructure.Persistence;
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
     ILogger<RestClientService> logger,
     IntegrationLogsService logService) : RestClientService(settings, logger), IKidanaClient
    {
        public ErrorOr<(KidanaResponseBase<KidanaDetailsResponse> Result, Guid LogId)> ValidateTicket(string kidanaNumber)
        {
            try
            {
                
                var request = new KidanaDetailsRequest { TicketId = kidanaNumber.ToUpper() };
                ErrorOr<KidanaResponseBase<KidanaDetailsResponse>> result;

                if (settings.UseTestData)
                {
                    result = LoadTestData();
                }
                else
                {
                    result = PrepareAndExecuteRequest<KidanaResponseBase<KidanaDetailsResponse>>(
                        resourceUrl: settings.ValidateTicketEndpoint,
                        method: Method.Post,
                        body: request,
                        resourceParameters: settings.DefaultParams?
                            .Select(kvp => ResourceParameter.Create(
                                kvp.Key,
                                kvp.Value,
                                ParameterType.QueryString)).ToList() ?? new List<ResourceParameter>()
                    ).Match(
                        response => response.EnsureSuccessResult(),
                        error => error
                    );
                }

                // Create log after getting the result
                var logId = logService.CreateCompleteLog(
                    kidanaNumber,
                    request,
                    result,
                    ldv_integrationlogs.IntegrationTypeCode_OptionSet.Kidana,
                    ldv_integrationlogs.IntegrationOperationCode_OptionSet.Validate);

                return (result.Value, logId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process Kidana request");
                return Error.Unexpected("KIDANA_PROCESSING_ERROR", ex.Message);
            }

        }


        private ErrorOr<KidanaResponseBase<KidanaDetailsResponse>> LoadTestData()
        {
            try
            {
                var json = File.ReadAllText(settings.TestDataPath);
                var response = JsonConvert.DeserializeObject<KidanaResponseBase<KidanaDetailsResponse>>(json);

                return response?.EnsureSuccessResult() ?? Error.NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load test data");
                return Error.Unexpected("TEST_DATA_ERROR", ex.Message);
            }
        }
    }
}

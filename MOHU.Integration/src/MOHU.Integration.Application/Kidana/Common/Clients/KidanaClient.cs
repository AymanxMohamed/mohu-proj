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
        public ErrorOr<(KidanaDetailsResponse Result, Guid LogId)> ValidateTicket(string kidanaNumber)
        {
            try
            {

                var request = new KidanaDetailsRequest { TicketId = kidanaNumber.ToUpper() };
                ErrorOr<KidanaDetailsResponse> result;

                if (settings.UseTestData)
                {
                    result = LoadTestData();
                }
                else
                {
                    result = PrepareAndExecuteRequest<KidanaDetailsResponse>(
                        resourceUrl: settings.ValidateTicketEndpoint,
                        method: Method.Post,
                        body: request,
                        resourceParameters: settings.DefaultParams?
                            .Select(kvp => ResourceParameter.Create(
                                kvp.Key,
                                kvp.Value.Value,
                                ParameterType.QueryString)).ToList() ?? new List<ResourceParameter>()
                    ).Match<ErrorOr<KidanaDetailsResponse>>(
                        response =>
                        {
                            if (response.Msg == "Ticket not Found")
                            {
                                return Error.Validation("TICKET_NOT_FOUND", "Ticket not found");
                            }
                            return string.IsNullOrEmpty(response.Status)
                                ? Error.Validation("INVALID_RESPONSE", "Missing status")
                                : response;
                        },
                        errors => errors
                    );
                }

                // Create log after getting the result
                var logId = logService.CreateCompleteLog(
                    kidanaNumber,
                    request,
                    result,
                    ldv_integrationlogs.IntegrationTypeCode_OptionSet.Kidana,
                    ldv_integrationlogs.IntegrationOperationCode_OptionSet.Validate);

                return result.Match<ErrorOr<(KidanaDetailsResponse, Guid)>>(
                    success => (success, logId),
                    errors => errors
                );

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process Kidana request");
                return Error.Unexpected("KIDANA_PROCESSING_ERROR", ex.Message);
            }

        }

        private ErrorOr<KidanaDetailsResponse> LoadTestData()
        {
            try
            {
                var json = File.ReadAllText(settings.TestDataPath);
                // Assume valid response
                return JsonConvert.DeserializeObject<KidanaDetailsResponse>(json)!;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load test data");
                return Error.Unexpected("TEST_DATA_ERROR", ex.Message);
            }
        }

    }

}

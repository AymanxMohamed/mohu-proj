using MOHU.Integration.Contracts.Dto.Activity;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.Ivr;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Application.Service
{
    public class IvrService : IIvrService
    {
        private readonly IIndividualService _individualService;
        private readonly IActivityService _activityService;
        private readonly IConfigurationService _configurationService;

        public IvrService(IIndividualService individualService, IActivityService activityService, IConfigurationService configurationService)
        {
            _individualService = individualService;
            _activityService = activityService;
            _configurationService = configurationService;
        }
        public async Task<string> GetCustomerProfileUrlAsync(GetCustomerProfileRequest request)
        {
            var individual = await _individualService.GetIndividualByMobileNumberAsync(request.MobileNumber);

            individual ??= await _individualService.CreateIndividualAsync(request.MobileNumber);

            var createActivityRequest = new CreateActivityRequest()
            {
                ActivityName = PhoneCall.EntityLogicalName,
                Owner = new LookupDto(Guid.Parse("eacc971e-11c1-ee11-9079-6045bd895c76"), "systemuser"),
                From = new LookupDto(individual.Id, Contact.EntityLogicalName),
            };
            //createActivityRequest.ExtraProperties.Add(PhoneCall.Fields.IvrInteractionNumber, request.IvrInteractionNumber);

            var phoneCall = await _activityService.CreateActivityAsync(createActivityRequest);

            var applicationId = await _configurationService.GetConfigurationValueAsync("CallCenterAppId");

            return string.Format(await _configurationService.GetConfigurationValueAsync("IndividualProfileUrl"), applicationId, individual.Id);

        }
    }
}

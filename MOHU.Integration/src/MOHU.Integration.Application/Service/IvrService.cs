using MOHU.Integration.Application.Exceptions;
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
        private readonly IUserService _userService;

        public IvrService(IIndividualService individualService, IActivityService activityService, IConfigurationService configurationService, IUserService userService)
        {
            _individualService = individualService;
            _activityService = activityService;
            _configurationService = configurationService;
            _userService = userService;
        }
        public async Task<string> GetCustomerProfileUrlAsync(GetCustomerProfileRequest request)
        {
            var individual = await _individualService.GetIndividualByMobileNumberAsync(request.MobileNumber);

            individual ??= await _individualService.CreateIndividualAsync(request.MobileNumber);

            var agent = await _userService.GetUserByUsernameAsync(request.AgentUserName)
                ?? throw new NotFoundException($"Agent with the following name {request.AgentUserName} does not exist");
            
            var createActivityRequest = new CreateActivityRequest()
            {
                ActivityName = PhoneCall.EntityLogicalName,
                Owner = agent,
                From = new LookupDto(individual.Id, Contact.EntityLogicalName),
            };
            createActivityRequest.ExtraProperties.Add(PhoneCall.Fields.ldv_IvrInteractionNumber, request.IvrInteractionNumber);
            createActivityRequest.ExtraProperties.Add(PhoneCall.Fields.Subject, $"{request.MobileNumber} - {request.IvrInteractionNumber}");
            createActivityRequest.ExtraProperties.Add(PhoneCall.Fields.DirectionCode, false);
            createActivityRequest.ExtraProperties.Add(PhoneCall.Fields.PhoneNumber, request.MobileNumber);

            var phoneCall = await _activityService.CreateActivityAsync(createActivityRequest);

            var applicationId = await _configurationService.GetConfigurationValueAsync("CallCenterAppId");

            return string.Format(await _configurationService.GetConfigurationValueAsync("IndividualProfileUrl"), applicationId, individual.Id);

        }
    }
}

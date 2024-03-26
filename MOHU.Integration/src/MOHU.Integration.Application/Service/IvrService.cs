using Microsoft.BusinessData.MetadataModel;
using Microsoft.Xrm.Sdk;
using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Dto.Activity;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.Ivr;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Common;
using MOHU.Integration.Contracts.Interface.Customer;
using MOHU.Integration.Domain.Entitiy;

namespace MOHU.Integration.Application.Service
{
    public class IvrService : IIvrService
    {
        private readonly ICustomerService _individualService;
        private readonly IActivityService _activityService;
        private readonly IConfigurationService _configurationService;
        private readonly IUserService _userService;

        public IvrService(ICustomerService individualService, IActivityService activityService, IConfigurationService configurationService, IUserService userService)
        {
            _individualService = individualService;
            _activityService = activityService;
            _configurationService = configurationService;
            _userService = userService;
        }

        public async Task<Guid> CreatePhoneCall(CreatePhoneCallRequest request)
        {
            var individual = await _individualService.GetIndividualByMobileNumberAsync(request.MobileNumber)
                ?? throw new NotFoundException("Customer with mobile number does not exist");

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
            createActivityRequest.ExtraProperties.Add(PhoneCall.Fields.RegardingObjectId, new EntityReference(individual.EntityLogicalName,individual.Id));

            var phoneCall = await _activityService.CreateActivityAsync(createActivityRequest);
            return phoneCall.Id;
        }

        public async Task<string> GetCustomerProfileUrlAsync(string mobileNumber)
        {
            var individual = await _individualService.GetIndividualByMobileNumberAsync(mobileNumber);

            individual ??= await _individualService.CreateProfilelAsync(mobileNumber);
            
            var applicationId = await _configurationService.GetConfigurationValueAsync("CallCenterAppId");

            return string.Format(await _configurationService.GetConfigurationValueAsync("IndividualProfileUrl"), applicationId, individual.Id);

        }
    }
}

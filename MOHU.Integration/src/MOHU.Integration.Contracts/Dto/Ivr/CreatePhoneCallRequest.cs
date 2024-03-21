namespace MOHU.Integration.Contracts.Dto.Ivr
{
    public class CreatePhoneCallRequest
    {
        public string MobileNumber { get; set; }
        public string IvrInteractionNumber { get; set; }
        public string AgentUserName { get; set; }
    }
}

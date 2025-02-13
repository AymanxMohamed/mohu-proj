namespace MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Common.Dtos.Responses;

public class BaseServiceDeskResponse
{
    public string ReturnCode { get; set; } = null!;
    
    public List<string> Messages { get; set; } = null!;
}
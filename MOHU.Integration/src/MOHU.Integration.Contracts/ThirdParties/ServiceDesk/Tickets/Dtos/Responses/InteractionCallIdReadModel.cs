using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;

public class InteractionCallIdReadModel
{
    public string CRMNumber { get; set; } = null!;
    
    public string CallID { get; set; } = null!;
}
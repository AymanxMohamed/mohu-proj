// ReSharper disable InconsistentNaming

using Newtonsoft.Json;

namespace MOHU.Integration.Contracts.ThirdParties.ServiceDesk.Tickets.Dtos.Responses;

public class InteractionResponse
{
    public List<string>? Description { get; set; }
    
    public string? Category { get; set; }
    
    public string? Phase { get; set; }
    
    [JsonProperty("CallID")]
    public string CallId { get; set; } = null!;
    
    public string? AffectedService { get; set; }
    
    public List<string>? AssignmentGroup { get; set; }

    public string? OpenedBy { get; set; }

    public DateTime? OpenTime { get; set; }
    
    public string? ServiceRecipient { get; set; }
    
    public bool? SLABreached { get; set; }
    
    [JsonProperty("CRMNumber")]
    public string CrmNumber { get; set; } = null!;
    
    public string? Status { get; set; }
    
    public string? Impact { get; set; }
    
    public string? Priority { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? Title { get; set; }

    public string? Nationality { get; set; }

    public string? Contact { get; set; }
    
    public string? Area { get; set; }
    
    public string? Subcategory { get; set; }
    
    public string? PassportNumber { get; set; }
    
    public long? PhoneNumber { get; set; }
    
    public string? NotifyBy { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Urgency { get; set; }
}
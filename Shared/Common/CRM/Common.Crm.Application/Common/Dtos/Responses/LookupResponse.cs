using Microsoft.Xrm.Sdk;

namespace Common.Crm.Application.Common.Dtos.Responses;

public class LookupResponse<TId>
{
    private LookupResponse(TId id, string name)
    {
        Id = id;
        Name = name;
    }

    public TId Id { get; init; }

    public string Name { get; init; }

    public static LookupResponse<Guid>? Create(EntityReference? entityReference)
        => entityReference is null 
            ? null 
            : new LookupResponse<Guid>(entityReference.Id, entityReference.Name);
    
    public static LookupResponse<int>? Create<TEnum>(TEnum? enumValue)
        where TEnum : struct, Enum
        => enumValue is null 
            ? null 
            : new LookupResponse<int>(Convert.ToInt32(enumValue.Value), enumValue.Value.ToString());
}
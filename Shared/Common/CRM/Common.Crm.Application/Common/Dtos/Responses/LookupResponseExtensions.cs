using Microsoft.Xrm.Sdk;

namespace Common.Crm.Application.Common.Dtos.Responses;

public static class LookupResponseExtensions
{
    public static LookupResponse<Guid>? ToLookup(this EntityReference? entityReference) => 
        LookupResponse<Guid>.Create(entityReference);
    
    public static LookupResponse<int>? ToLookup<TEnum>(this TEnum? enumValue) 
        where TEnum : struct, Enum => LookupResponse<int>.Create(enumValue);
}
using Microsoft.Xrm.Sdk;

namespace MOHU.Integration.Contracts.Services;

public class Service
{
    public EntityReference Process { get; private init; } = null!;

    public EntityReference ParentService { get; private init; } = null!;

    public static Service Create(EntityReference process, EntityReference parentService)
    {
        return new Service
        {
            Process = process,
            ParentService = parentService
        };
    }
}
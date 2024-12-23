using Microsoft.Xrm.Sdk;

namespace MOHU.Integration.Contracts.Services;

public class Service
{
    public EntityReference? Process { get; private init; }

    public EntityReference? ParentService { get; private init; }

    public static Service Create(EntityReference? process, EntityReference? parentService)
    {
        return new Service
        {
            Process = process,
            ParentService = parentService
        };
    }
}
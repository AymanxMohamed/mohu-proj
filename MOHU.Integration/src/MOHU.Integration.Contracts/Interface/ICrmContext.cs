using Microsoft.PowerPlatform.Dataverse.Client;

namespace MOHU.Integration.Contracts.Interface
{

    public interface ICrmContext
    {
        ServiceClient ServiceClient { get; }
    }


}

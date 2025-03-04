
using Newtonsoft.Json;

namespace Common.Crm.Infrastructure.Common.Serialization;

public class AppSerializer : IJsonSerializer
{
    private static readonly Lazy<IJsonSerializer> SInstance = new(() => new AppSerializer());

    public static IJsonSerializer Instance => SInstance.Value;

    private AppSerializer()
    {
    }
        
    public string Serialize(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public T? Deserialize<T>(string obj)
    {
        return JsonConvert.DeserializeObject<T>(obj);
    }
}
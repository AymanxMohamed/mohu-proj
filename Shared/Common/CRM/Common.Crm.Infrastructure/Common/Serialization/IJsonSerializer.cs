namespace Common.Crm.Infrastructure.Common.Serialization;

public interface IJsonSerializer
{
    string Serialize(object obj);
    
    T? Deserialize<T>(string obj);
}
using Newtonsoft.Json;

namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;

public class FilterItem
{
    public FilterItem()
    {
        
    }
    
    private FilterItem(string propertyName, string operation, object propertyValue)
    {
        PropertyName = propertyName;
        Operation = operation;
        PropertyValue = propertyValue;
    }
    
    [JsonProperty("propertyName")]
    public string PropertyName { get; set; } = null!;
    
    [JsonProperty("operation")]
    public string Operation { get; set; } = null!;
    
    
    [JsonProperty("propertyValue")]
    public object PropertyValue { get; set; } = null!;
    
    public static FilterItem Create(string propertyName, string operation, object propertyValue)
        => new(propertyName, operation, propertyValue);
}
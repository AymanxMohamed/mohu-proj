namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;

public class FilterItem
{
    private FilterItem(string propertyName, string operation, object propertyValue)
    {
        PropertyName = propertyName;
        Operation = operation;
        PropertyValue = propertyValue;
    }

    public string PropertyName { get; set; }
    
    public string Operation { get; set; } 
    
    public object PropertyValue { get; set; }
    
    public static FilterItem Create(string propertyName, string operation, object propertyValue)
        => new(propertyName, operation, propertyValue);
}
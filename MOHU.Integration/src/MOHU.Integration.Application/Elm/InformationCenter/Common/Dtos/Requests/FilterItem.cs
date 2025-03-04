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

    public string PropertyName { get; set; } = null!;
    
    public string Operation { get; set; } = null!;
    
    public object PropertyValue { get; set; } = null!;
    
    public static FilterItem Create(string propertyName, string operation, object propertyValue)
        => new(propertyName, operation, propertyValue);
}
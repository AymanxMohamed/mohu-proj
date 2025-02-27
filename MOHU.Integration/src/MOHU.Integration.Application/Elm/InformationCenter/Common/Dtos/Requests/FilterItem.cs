namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;

public class FilterItem
{
    public required string PropertyName { get; set; }
    public required string Operation { get; set; } 
    public required object PropertyValue { get; set; }
}
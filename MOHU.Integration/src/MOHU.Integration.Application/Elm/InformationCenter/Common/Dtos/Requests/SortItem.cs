namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;

public class SortItem
{
    private SortItem(string propertyName, string orderDir)
    {
        PropertyName = propertyName;
        OrderDir = orderDir;
    }
    
    public string PropertyName { get; set; }
    
    public string OrderDir { get; set; }

    public static SortItem Create(string propertyName, string orderDir)
        => new(propertyName, orderDir);
}
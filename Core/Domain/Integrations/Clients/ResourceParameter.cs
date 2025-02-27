namespace Core.Domain.Integrations.Clients;

public partial class ResourceParameter : IEquatable<ResourceParameter>
{
    private ResourceParameter()
    {
    }

    public string Name { get; private init; } = null!;

    public object? Value { get; private init; }

    public ParameterType ParameterType { get; set; }

    public static ResourceParameter Create(
        string name, 
        object? value = null, 
        ParameterType parameterType = ParameterType.QueryString)
    {
        return new ResourceParameter
        {
            Name = name,
            Value = value,
            ParameterType = parameterType
        };
    }

    public static List<ResourceParameter> CreateList<T>(
        string name,
        IEnumerable<T>? values = null,
        ParameterType parameterType = ParameterType.QueryString)
    {
        return values?.Select(x => Create(name, x, parameterType)).ToList() ?? [];
    }
}
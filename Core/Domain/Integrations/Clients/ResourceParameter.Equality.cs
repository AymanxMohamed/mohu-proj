namespace Core.Domain.Integrations.Clients;

public partial class ResourceParameter
{
    public bool Equals(ResourceParameter? other)
    {
        return other is not null && Name == other.Name && ParameterType == other.ParameterType;
    }

    public override string ToString() => Name;
}
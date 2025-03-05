namespace MOHU.Integration.Domain.Features.Common.CrmEntities;

public partial class CrmEntity : IEquatable<CrmEntity>
{
    public static bool operator ==(CrmEntity left, CrmEntity right) => left.Equals(right);
    
    public static bool operator !=(CrmEntity left, CrmEntity right) => !left.Equals(right);

    public bool Equals(CrmEntity? other) => 
        other is not null && other.Id.Id != Guid.Empty && Id.Id == other.Id.Id;
    
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            CrmEntity entity => Equals(entity),
            _ => false
        };
    }

    public override int GetHashCode() => Id.GetHashCode();
}
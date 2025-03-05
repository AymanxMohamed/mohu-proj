using MOHU.Integration.Domain.Features.Common.CrmEntities;

namespace MOHU.Integration.Domain.Features.Common.ElmReferencedEntities;

public partial class CrmElmReferencedEntity : IEquatable<CrmElmReferencedEntity>
{
    public static bool operator ==(CrmElmReferencedEntity left, CrmElmReferencedEntity right) => ((CrmEntity)left).Equals(right);
    
    public static bool operator !=(CrmElmReferencedEntity left, CrmElmReferencedEntity right) => !((CrmEntity)left).Equals(right);

    public bool Equals(CrmElmReferencedEntity? other) => 
        other is not null && (
            ElmReferenceId == other.ElmReferenceId
            || base.Equals(other));
    

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            CrmElmReferencedEntity country => Equals(country),
            _ => false
        };
    }

    public override int GetHashCode() => Id.GetHashCode();
}
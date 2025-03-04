namespace MOHU.Integration.Domain.Individuals;

public partial class Individual : IEquatable<Individual>
{
    public static bool operator ==(Individual left, Individual right) => left.Equals(right);
    
    public static bool operator !=(Individual left, Individual right) => !left.Equals(right);

    public bool Equals(Individual? other) => 
        other is not null && (
            IntegrationDetails.ElmReferenceId == other.IntegrationDetails.ElmReferenceId
            || Id.Id == other.Id.Id);
    

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Individual individual => Equals(individual),
            _ => false
        };
    }

    public override int GetHashCode() => Id.GetHashCode();
}
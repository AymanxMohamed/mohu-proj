namespace MOHU.Integration.Domain.Features.Countries;

public partial class Country : IEquatable<Country>
{
    public static bool operator ==(Country left, Country right) => left.Equals(right);
    
    public static bool operator !=(Country left, Country right) => !left.Equals(right);

    public bool Equals(Country? other) => 
        other is not null && (
            ElmReferenceId == other.ElmReferenceId
            || base.Equals(other));
    

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Country country => Equals(country),
            _ => false
        };
    }

    public override int GetHashCode() => Id.GetHashCode();
}
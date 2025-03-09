namespace MOHU.Integration.Domain.Features.Companies;

public partial class Company : IEquatable<Company>
{
    public static bool operator ==(Company left, Company right) => left.Equals(right);
    
    public static bool operator !=(Company left, Company right) => !left.Equals(right);

    public bool Equals(Company? other) => 
        other is not null && (
            ElmReferenceId == other.ElmReferenceId
            || string.Equals(OrganizationArabicName, other.OrganizationArabicName, StringComparison.OrdinalIgnoreCase) 
            || string.Equals(OrganizationEnglishName, other.OrganizationEnglishName, StringComparison.OrdinalIgnoreCase) 
            || string.Equals(SicCode, other.SicCode, StringComparison.OrdinalIgnoreCase) 
            || string.Equals(SicCode, other.ElmReferenceId.ToString(), StringComparison.OrdinalIgnoreCase) 
            || base.Equals(other));
    

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Company company => Equals(company),
            _ => false
        };
    }

    public override int GetHashCode() => Id.GetHashCode();
}
using MOHU.Integration.Domain.Features.Companies;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Houses.Dtos.Responses;

public partial class ElmHouseResponse : IEquatable<Company>, IEquatable<ElmHouseResponse>
{
    public static bool operator ==(ElmHouseResponse left, Company right) => left.Equals(right);

    public static bool operator !=(ElmHouseResponse left, Company right) => !left.Equals(right);

    public bool Equals(Company? other) => other is not null && (
        Id == other.ElmReferenceId
        || string.Equals(ArabicName, other.OrganizationArabicName, StringComparison.OrdinalIgnoreCase)
        || string.Equals(EnglishName, other.OrganizationEnglishName, StringComparison.OrdinalIgnoreCase)
        || string.Equals(Id.ToString(), other.SicCode, StringComparison.OrdinalIgnoreCase));

    public bool Equals(ElmHouseResponse? other) => other is not null && Id == other.Id;

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Company company => Equals(company),
            ElmHouseResponse elmHouseResponse => Equals(elmHouseResponse),
            _ => false
        };
    }

    public override int GetHashCode() => Id.GetHashCode();
}
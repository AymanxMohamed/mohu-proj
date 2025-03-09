using MOHU.Integration.Domain.Features.Companies;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.Common.Dtos.Responses;

public partial class ElmCompanyResponse : IEquatable<Company>, IEquatable<ElmCompanyResponse>
{
    public static bool operator ==(ElmCompanyResponse left, Company right) => left.Equals(right);

    public static bool operator !=(ElmCompanyResponse left, Company right) => !left.Equals(right);

    public bool Equals(Company? other) => other is not null && (
        Id == other.ElmReferenceId
        || string.Equals(ArabicName, other.OrganizationArabicName, StringComparison.OrdinalIgnoreCase)
        || string.Equals(EnglishName, other.OrganizationEnglishName, StringComparison.OrdinalIgnoreCase)
        || string.Equals(Id.ToString(), other.SicCode, StringComparison.OrdinalIgnoreCase));

    public bool Equals(ElmCompanyResponse? other) => other is not null && Id == other.Id;

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Company company => Equals(company),
            ElmCompanyResponse elmCompanyResponse => Equals(elmCompanyResponse),
            _ => false
        };
    }

    public override int GetHashCode() => Id.GetHashCode();
}
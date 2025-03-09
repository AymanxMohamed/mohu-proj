using MOHU.Integration.Domain.Features.Companies;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Companies.HajMissionsCompanies.Dtos.Responses;

public partial class ElmHajMissionCompanyResponse : IEquatable<Company>, IEquatable<ElmHajMissionCompanyResponse>
{
    public static bool operator ==(ElmHajMissionCompanyResponse left, Company right) => left.Equals(right);

    public static bool operator !=(ElmHajMissionCompanyResponse left, Company right) => !left.Equals(right);

    public bool Equals(Company? other) => other is not null && (
        Id == other.ElmReferenceId
        || string.Equals(ArabicName, other.OrganizationArabicName, StringComparison.OrdinalIgnoreCase)
        || string.Equals(EnglishName, other.OrganizationEnglishName, StringComparison.OrdinalIgnoreCase)
        || string.Equals(Id.ToString(), other.SicCode, StringComparison.OrdinalIgnoreCase));

    public bool Equals(ElmHajMissionCompanyResponse? other) => other is not null && Id == other.Id;

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Company company => Equals(company),
            ElmHajMissionCompanyResponse elmHajMissionCompanyResponse => Equals(elmHajMissionCompanyResponse),
            _ => false
        };
    }

    public override int GetHashCode() => Id.GetHashCode();
}
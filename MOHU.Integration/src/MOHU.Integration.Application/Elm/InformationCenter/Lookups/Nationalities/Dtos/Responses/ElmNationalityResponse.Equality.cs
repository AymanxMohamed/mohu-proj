using MOHU.Integration.Domain.Features.Countries;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Nationalities.Dtos.Responses;

public partial class ElmNationalityResponse : IEquatable<Country>, IEquatable<ElmNationalityResponse>
{
    public static bool operator ==(ElmNationalityResponse left, Country right) => left.Equals(right);

    public static bool operator !=(ElmNationalityResponse left, Country right) => !left.Equals(right);

    public bool Equals(Country? other) => other is not null && (
        Id == other.ElmReferenceId
        || string.Equals(ArabicName, other.ArabicName, StringComparison.OrdinalIgnoreCase)
        || string.Equals(EnglishName, other.EnglishName, StringComparison.OrdinalIgnoreCase));

    public bool Equals(ElmNationalityResponse? other) => other is not null && Id == other.Id;

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Country nationality => Equals(nationality),
            ElmNationalityResponse elmNationalityResponse => Equals(elmNationalityResponse),
            _ => false
        };
    }

    public override int GetHashCode() => Id.GetHashCode();
}
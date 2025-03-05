using MOHU.Integration.Domain.Features.Countries;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Dtos.Responses;

public partial class ElmCountryResponse : IEquatable<Country>, IEquatable<ElmCountryResponse>
{
    public static bool operator ==(ElmCountryResponse left, Country right) => left.Equals(right);
    
    public static bool operator !=(ElmCountryResponse left, Country right) => !left.Equals(right);

    public bool Equals(Country? other) => other is not null && (
        Id == other.ElmReferenceId
        || string.Equals(ArabicName, other.ArabicName, StringComparison.OrdinalIgnoreCase)
        || string.Equals(EnglishName, other.EnglishName, StringComparison.OrdinalIgnoreCase));
    
    public bool Equals(ElmCountryResponse? other) => other is not null && Id == other.Id;

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Country country => Equals(country),
            ElmCountryResponse elmCountryResponse => Equals(elmCountryResponse),
            _ => false
        };
    }

    public override int GetHashCode() => Id.GetHashCode();
}
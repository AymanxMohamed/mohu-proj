namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.ContactInformation;

public partial class ElmApplicantPhoneNumber : IEquatable<string>, IEquatable<ElmApplicantPhoneNumber>
{
    public static bool operator ==(ElmApplicantPhoneNumber left, string right) =>
        left.Equals(right);
    
    public static bool operator !=(ElmApplicantPhoneNumber left, string right) => 
        !left.Equals(right);

    public bool Equals(string? other) =>
        other is not null
        && MobileNumber is not null  
        && MobileCountryCode is not null  
        && FullNumber.Equals(other, StringComparison.OrdinalIgnoreCase);
    
    public bool Equals(ElmApplicantPhoneNumber? other) => 
        other is not null 
        && MobileNumber is not null  
        && MobileCountryCode is not null  
        && FullNumber.Equals(other.FullNumber, StringComparison.OrdinalIgnoreCase);

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            string str => Equals(str),
            ElmApplicantPhoneNumber elmApplicantPhoneNumber => Equals(elmApplicantPhoneNumber),
            _ => false
        };
    }

    public override int GetHashCode() => FullNumber.ToLowerInvariant().GetHashCode();
}
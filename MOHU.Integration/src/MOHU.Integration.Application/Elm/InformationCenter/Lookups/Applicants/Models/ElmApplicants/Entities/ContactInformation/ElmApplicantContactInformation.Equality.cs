using MOHU.Integration.Domain.Features.Individuals.Entities;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.ContactInformation;

public partial class ElmApplicantContactInformation : IEquatable<IndividualContactInformation>, IEquatable<ElmApplicantContactInformation>
{
    public static bool operator ==(ElmApplicantContactInformation left, IndividualContactInformation right) =>
        left.Equals(right);
    
    public static bool operator !=(ElmApplicantContactInformation left, IndividualContactInformation right) => 
        !left.Equals(right);

    public bool Equals(IndividualContactInformation? other) =>
        other is not null 
        && Email is not null 
        && Email.Equals(other.Email, StringComparison.OrdinalIgnoreCase);
    
    public bool Equals(ElmApplicantContactInformation? other) => 
        other is not null
        && Email is not null 
        && Email.Equals(other.Email, StringComparison.OrdinalIgnoreCase);

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            IndividualContactInformation individualContactInformation => Equals(individualContactInformation),
            ElmApplicantContactInformation elmApplicantContactInformation => Equals(elmApplicantContactInformation),
            _ => false
        };
    }

    public override int GetHashCode() => Email?.ToLowerInvariant().GetHashCode() ?? 0;
}
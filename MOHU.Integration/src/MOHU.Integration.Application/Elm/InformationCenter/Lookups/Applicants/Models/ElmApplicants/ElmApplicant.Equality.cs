using Individual = MOHU.Integration.Domain.Individuals.Individual;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants;

public partial class ElmApplicant : IEquatable<Individual>, IEquatable<ElmApplicant>
{
    public static bool operator ==(ElmApplicant left, Individual right) => left.Equals(right);
    
    public static bool operator !=(ElmApplicant left, Individual right) => !left.Equals(right);

    public bool Equals(Individual? other) => 
        other is not null && Id == other.IntegrationDetails.ElmReferenceId;
    
    public bool Equals(ElmApplicant? other) => 
        other is not null && Id == other.Id;

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Individual individual => Equals(individual),
            ElmApplicant elmApplicant => Equals(elmApplicant),
            _ => false
        };
    }

    public override int GetHashCode() => Id.GetHashCode();
}
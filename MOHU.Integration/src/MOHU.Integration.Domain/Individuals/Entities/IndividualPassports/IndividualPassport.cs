namespace MOHU.Integration.Domain.Individuals.Entities.IndividualPassports;

public class IndividualPassport
{
    public string Number { get; init; } = null!;

    public string PasswordType { get; init; }
    
    public string PassportIssuePlace { get; init; }
    
    public string PassportIssueDate { get; init; }
    
    public string PassportExpiryDate { get; init; }
}
namespace MOHU.Integration.Domain.Individuals;

public class Individual
{
    public Individual()
    {
    }

    private Individual(Entity entity)
    {
    }
    
    public Guid Id { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }
    
    public string? EnglishName { get; init; }
    
    public string? ArabicName { get; init; }
    
    public string? PhoneNumber { get; init; }
    
    public string? OriginCode { get; init; }

    public DateTime BirthDate { get; init; }
    
    public int ElmReferenceId { get; init; }

    public string? PassportNumber { get; init; }
    
    public string? HijriBirthDate { get; init; }

    public string? Email { get; init; }
    

    public static Individual Create(Entity entity)
    {
        return new Individual();
    }
}
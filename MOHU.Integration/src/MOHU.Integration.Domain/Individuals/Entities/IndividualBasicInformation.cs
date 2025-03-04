using Common.Crm.Domain.Common.OptionSets.Extensions;
using MOHU.Integration.Domain.Individuals.Constants;

namespace MOHU.Integration.Domain.Individuals.Entities;

public class IndividualBasicInformation
{
    private IndividualBasicInformation(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(IndividualBasicInformation), IndividualConstants.LogicalName);
        
        FirstName = entity.GetAttributeValue<string>(IndividualConstants.Fields.BasicInformation.FirstName);
        
        LastName = entity.GetAttributeValue<string>(IndividualConstants.Fields.BasicInformation.LastName);
        
        EnglishName = entity.GetAttributeValue<string>(IndividualConstants.Fields.BasicInformation.EnglishName);
        
        ArabicName = entity.GetAttributeValue<string>(IndividualConstants.Fields.BasicInformation.ArabicName);
        
        Gender = entity
            .GetOptionSetValue(IndividualConstants.Fields.BasicInformation.Gender)
            .ToEnum<GenderEnum>();
        
        MartialStatus = entity
            .GetOptionSetValue(IndividualConstants.Fields.BasicInformation.MartialStatus)
            .ToEnum<MartialStatusEnum>();
    }

    private IndividualBasicInformation(
        string? firstName, 
        string? lastName, 
        string? englishName, 
        string? arabicName, 
        GenderEnum? gender, 
        MartialStatusEnum? martialStatus)
    {
        FirstName = firstName;
        LastName = lastName;
        EnglishName = englishName;
        ArabicName = arabicName;
        Gender = gender;
        MartialStatus = martialStatus;
    }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }
    
    public string? EnglishName { get; init; }
    
    public string? ArabicName { get; init; }
    
    public GenderEnum? Gender { get; init; }
    
    public MartialStatusEnum? MartialStatus { get; init; }
    
    public static IndividualBasicInformation Create(Entity entity) => new(entity);

    public static IndividualBasicInformation Create(
        string? firstName,
        string? lastName,
        string? englishName,
        string? arabicName,
        GenderEnum? gender,
        MartialStatusEnum? martialStatus) => 
        new(firstName, lastName, englishName, arabicName, gender, martialStatus);
}
using MOHU.Integration.Domain.Individuals.Entities;

namespace MOHU.Integration.Domain.Individuals;

public class Individual
{
    private Individual(Entity entity)
        : this(
            entity.ToEntityReference(),
            IndividualBasicInformation.Create(entity),
            IndividualBirthInformation.Create(entity),
            IndividualContactInformation.Create(entity),
            IndividualNationalityDetails.Create(entity),
            IndividualIntegrationDetails.Create(entity),
            IndividualIdentification.Create(entity),
            IndividualVisaDetails.Create(entity))
    {
    }

    private Individual(
        EntityReference id, 
        IndividualBasicInformation basicInformation, 
        IndividualBirthInformation birthInformation, 
        IndividualContactInformation contactInformation, 
        IndividualNationalityDetails nationalityDetails, 
        IndividualIntegrationDetails integrationDetails, 
        IndividualIdentification identification, 
        IndividualVisaDetails visaDetails)
    {
        Id = id;
        BasicInformation = basicInformation;
        BirthInformation = birthInformation;
        ContactInformation = contactInformation;
        NationalityDetails = nationalityDetails;
        IntegrationDetails = integrationDetails;
        Identification = identification;
        VisaDetails = visaDetails;
    }

    public EntityReference Id { get; init; }
    
    public IndividualBasicInformation BasicInformation { get; init; }
    
    public IndividualBirthInformation BirthInformation { get; init; }
    
    public IndividualContactInformation ContactInformation { get; init; }
    
    public IndividualNationalityDetails NationalityDetails { get; init; }
    
    public IndividualIntegrationDetails IntegrationDetails { get; init; }
    
    public IndividualIdentification Identification { get; init; }
    
    public IndividualVisaDetails VisaDetails { get; init; }

    public static Individual Create(Entity entity) => new(entity);

    public static Individual Create(
        EntityReference id, 
        IndividualBasicInformation basicInformation, 
        IndividualBirthInformation birthInformation, 
        IndividualContactInformation contactInformation, 
        IndividualNationalityDetails nationalityDetails, 
        IndividualIntegrationDetails integrationDetails, 
        IndividualIdentification identification, 
        IndividualVisaDetails visaDetails) => new(
        id,
        basicInformation, 
        birthInformation,
        contactInformation, 
        nationalityDetails,
        integrationDetails, 
        identification,
        visaDetails);
}
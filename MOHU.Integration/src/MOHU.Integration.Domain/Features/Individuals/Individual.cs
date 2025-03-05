using Common.Crm.Domain.Common.Factories;
using MOHU.Integration.Domain.Features.Common.CrmEntities;
using MOHU.Integration.Domain.Features.Individuals.Constants;
using MOHU.Integration.Domain.Features.Individuals.Entities;

namespace MOHU.Integration.Domain.Features.Individuals;

public partial class Individual : CrmEntity
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
        : base(id)
    {
        BasicInformation = basicInformation;
        BirthInformation = birthInformation;
        ContactInformation = contactInformation;
        NationalityDetails = nationalityDetails;
        IntegrationDetails = integrationDetails;
        Identification = identification;
        VisaDetails = visaDetails;
    }

    public IndividualBasicInformation BasicInformation { get; init; }
    
    public IndividualBirthInformation BirthInformation { get; init; }
    
    public IndividualContactInformation ContactInformation { get; init; }
    
    public IndividualNationalityDetails NationalityDetails { get; init; }
    
    public IndividualIntegrationDetails IntegrationDetails { get; init; }
    
    public IndividualIdentification Identification { get; init; }
    
    public IndividualVisaDetails VisaDetails { get; init; }

    public static Individual Create(Entity entity) => new(entity);

    public static Individual Create(
        EntityReference? id, 
        IndividualBasicInformation basicInformation, 
        IndividualBirthInformation birthInformation, 
        IndividualContactInformation contactInformation, 
        IndividualNationalityDetails nationalityDetails, 
        IndividualIntegrationDetails integrationDetails, 
        IndividualIdentification identification, 
        IndividualVisaDetails visaDetails) => new(
        id ?? EntityReferenceFactory.Create(IndividualConstants.LogicalName),
        basicInformation, 
        birthInformation,
        contactInformation, 
        nationalityDetails,
        integrationDetails, 
        identification,
        visaDetails);

    public new Entity ToCrmEntity()
    {
        var entity = base.ToCrmEntity();
        
        BasicInformation.UpdateEntity(entity);
        BirthInformation.UpdateEntity(entity);
        ContactInformation.UpdateEntity(entity);
        NationalityDetails.UpdateEntity(entity);
        IntegrationDetails.UpdateEntity(entity);
        Identification.UpdateEntity(entity);
        VisaDetails.UpdateEntity(entity);

        return entity;
    }

}
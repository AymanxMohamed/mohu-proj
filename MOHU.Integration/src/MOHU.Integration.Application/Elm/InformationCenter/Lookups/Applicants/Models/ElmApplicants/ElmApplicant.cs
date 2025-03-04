using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.BasicInformation;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.BirthInformation;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.ContactInformation;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Identification;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Nationality;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.VisaDetails;
using MOHU.Integration.Domain.Individuals.Entities;
using MOHU.Integration.Domain.Individuals.Enums;
using Individual = MOHU.Integration.Domain.Individuals.Individual;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants;

public class ElmApplicant
{
    private ElmApplicant(ApplicantResponse applicant)
    {
        Id = applicant.Id;
        
        BasicInformation = ElmApplicantBasicInformation.Create(applicant);
        Identification = ElmApplicantIdentification.Create(applicant);
        Nationality = ElmApplicantNationality.Create(applicant);

        BirthInformation = ElmApplicantBirthInformation.Create(applicant);
        ContactInformation = ElmApplicantContactInformation.Create(applicant);
        VisaDetails = ElmApplicantVisaDetails.Create(applicant);
        
    }
    
    public int Id { get; init; }

    public ElmApplicantBasicInformation BasicInformation { get; init; }
    
    public ElmApplicantBirthInformation BirthInformation { get; init; }
    
    public ElmApplicantContactInformation ContactInformation { get; init; }
    
    public ElmApplicantIdentification Identification { get; init; }
    
    public ElmApplicantNationality Nationality { get; init; }
    
    public ElmApplicantVisaDetails VisaDetails { get; init; }
    
    public static ElmApplicant Create(ApplicantResponse applicant) => new(applicant);

    public static implicit operator ElmApplicant(ApplicantResponse applicant) => new(applicant);

    public Entity ToEntity(EntityReference? id = null) => ToIndividual(id).ToEntity();

    public Individual ToIndividual(EntityReference? id = null) =>
        Individual.Create(
            id: id,
            basicInformation: BasicInformation.ToIndividualInformation(),
            birthInformation: BirthInformation.ToIndividualInformation(),
            contactInformation: ContactInformation.ToIndividualInformation(),
            nationalityDetails: Nationality.ToIndividualInformation(),
            integrationDetails: GetIntegrationDetails(),
            identification: Identification.ToIndividualInformation(),
            visaDetails: VisaDetails.ToIndividualInformation());

    private IndividualIntegrationDetails GetIntegrationDetails() => IndividualIntegrationDetails
        .Create(
            originCode: OriginEnum.ElmInformationCenter,
            elmReferenceId: Id);
}
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.BasicInformation;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.BirthInformation;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.ContactInformation;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Identification;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Nationality;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.VisaDetails;
using MOHU.Integration.Domain.Features.Countries;
using MOHU.Integration.Domain.Features.Individuals.Entities;
using MOHU.Integration.Domain.Features.Individuals.Enums;
using Individual = MOHU.Integration.Domain.Features.Individuals.Individual;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants;

public partial class ElmApplicant : ElmEntity<Individual>
{
    private ElmApplicant(
        ApplicantResponse applicant, 
        Dictionary<int, Country> countries,
        Dictionary<int, Country> nationalities)
    {
        Id = applicant.Id;
        
        BasicInformation = ElmApplicantBasicInformation.Create(applicant);
        Identification = ElmApplicantIdentification.Create(applicant);
        Nationality = ElmApplicantNationality.Create(applicant, countries: countries, nationalities: nationalities);

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
    
    public static ElmApplicant Create(
        ApplicantResponse applicant, 
        Dictionary<int, Country> countries,
        Dictionary<int, Country> nationalities) => new(applicant, countries: countries, nationalities);

    public override Individual ToCrmEntity(EntityReference? id = null) =>
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
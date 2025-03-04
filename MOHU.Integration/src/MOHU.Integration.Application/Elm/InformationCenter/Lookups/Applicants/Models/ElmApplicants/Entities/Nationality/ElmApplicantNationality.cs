using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Domain.Individuals.Entities;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Nationality;

public class ElmApplicantNationality
{
    private ElmApplicantNationality(ApplicantResponse applicant)
    {
        CurrentNationalityId = applicant.AdCurrentNationalityId;
        ResidenceCountryId = applicant.AdResidenceCountryId;
    }
    
    public long? CurrentNationalityId { get; init; }
    
    public long? ResidenceCountryId { get; init; }

    public static ElmApplicantNationality Create(ApplicantResponse applicant) => new(applicant);
    
    internal IndividualNationalityDetails ToIndividualInformation() => IndividualNationalityDetails
        .Create(
            nationalityId:  null,
            countryOfResidence: null);
}
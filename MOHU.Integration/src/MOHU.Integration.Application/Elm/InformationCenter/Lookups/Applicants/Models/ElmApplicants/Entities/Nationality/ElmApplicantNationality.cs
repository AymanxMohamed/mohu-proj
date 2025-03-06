using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Domain.Features.Countries;
using MOHU.Integration.Domain.Features.Individuals.Entities;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.Nationality;

public class ElmApplicantNationality
{
    private ElmApplicantNationality(
        ApplicantResponse applicant, 
        Dictionary<int, Country> countries,
        Dictionary<int, Country> nationalities)
    {
        if (nationalities.TryGetValue(applicant.AdCurrentNationalityId, out var nationality))
        {
            CurrentNationalityId = nationality.Id;
        }
        
        if (applicant.AdResidenceCountryId.HasValue 
            && countries.TryGetValue(applicant.AdResidenceCountryId.Value, out var country))
        {
            ResidenceCountryId = country.Id;
        }
    }
    
    public EntityReference? CurrentNationalityId { get; init; }
    
    public EntityReference? ResidenceCountryId { get; init; }

    public static ElmApplicantNationality Create(
        ApplicantResponse applicant, 
        Dictionary<int, Country> countries,
        Dictionary<int, Country> nationalities) => new(applicant, countries, nationalities);
    
    internal IndividualNationalityDetails ToIndividualInformation() => IndividualNationalityDetails
        .Create(
            nationalityId:  CurrentNationalityId,
            countryOfResidence: ResidenceCountryId);
}
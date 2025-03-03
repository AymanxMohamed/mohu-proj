namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;

public class ElmApplicantBirthInformation
{
    private ElmApplicantBirthInformation(string? placeOfBirth, DateTime? birthDate, long? dateOfBirthHij, int? ageStageId)
    {
        PlaceOfBirth = placeOfBirth;
        BirthDate = birthDate;
        DateOfBirthHij = dateOfBirthHij;
        AgeStageId = ageStageId;
    }

    public string? PlaceOfBirth { get; init; }

    public DateTime? BirthDate { get; init; }

    public long? DateOfBirthHij { get; set; }
    
    public int? AgeStageId { get; set; }

    public static ElmApplicantBirthInformation Create(ApplicantResponse applicant)
        => new(
            applicant.AdPlaceOfBirth,
            applicant.AdDateOfBirth,
            applicant.AdDateOfBirthHij,
            applicant.AdAgeStageId);
}
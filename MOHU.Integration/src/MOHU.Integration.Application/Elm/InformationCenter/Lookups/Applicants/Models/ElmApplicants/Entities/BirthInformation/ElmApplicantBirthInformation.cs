using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Domain.Individuals.Entities;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.BirthInformation;

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
    
    public IndividualBirthInformation ToIndividualInformation() => IndividualBirthInformation
        .Create(
            placeOfBirth: PlaceOfBirth,
            birthDate: BirthDate,
            hijriBirthDate:DateOfBirthHij.ToString());
}
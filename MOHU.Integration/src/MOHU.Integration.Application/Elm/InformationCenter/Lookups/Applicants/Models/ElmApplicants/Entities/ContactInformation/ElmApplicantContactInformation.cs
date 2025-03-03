namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;

public class ElmApplicantContactInformation
{
    private ElmApplicantContactInformation(ApplicantResponse applicant)
    {
        Email = applicant.AdEmail;
        PhoneNumber = ElmApplicantPhoneNumber.Create(applicant);
    }

    public string? Email { get; init; }

    public ElmApplicantPhoneNumber PhoneNumber { get; init; }

    public static ElmApplicantContactInformation Create(ApplicantResponse applicant)
        => new(applicant);
}
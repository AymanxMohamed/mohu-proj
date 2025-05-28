using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Domain.Features.Individuals.Entities;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants.Entities.ContactInformation;

public partial class ElmApplicantContactInformation
{
    private const string ElmEmailPlaceholder = "dokumenvisa01@gmail.com";
    
    private ElmApplicantContactInformation(ApplicantResponse applicant)
    {
        Email = applicant.AdEmail;
        PhoneNumber = ElmApplicantPhoneNumber.Create(applicant);
    }

    public string? Email { get; init; }

    public ElmApplicantPhoneNumber PhoneNumber { get; init; }

    public static ElmApplicantContactInformation Create(ApplicantResponse applicant)
        => new(applicant);
    
    internal IndividualContactInformation ToIndividualInformation() => IndividualContactInformation
        .Create(
            email: null,
            mobileNumber: PhoneNumber.FullNumber);
}
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants;
using MOHU.Integration.Domain.Features.Countries;
using MOHU.Integration.Domain.Features.Individuals.Enums;
using Individual = MOHU.Integration.Domain.Features.Individuals.Individual;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;

public class ApplicantResponse : ElmEntity<Individual>
{
    public int AdApplicationNo { get; set; }
    
    public string? AdFirstNameEn { get; set; }
    
    public string? AdFatherNameEn { get; set; }
    
    public string? AdGrandFatherNameEn { get; set; }
    
    public string? AdFamilyNameEn { get; set; }
    
    public string? AdFirstNameAr { get; set; }
    
    public string? AdFatherNameAr { get; set; }
    
    public string? AdGrandFatherNameAr { get; set; }
    
    public string? AdFamilyNameAr { get; set; }
    
    public string? AdFullNameAr { get; set; }
    
    public string? AdFullNameEn { get; set; }
    
    public string? AdIdNo { get; set; }
    
    public string? AddIdSource { get; set; }
    
    public DateTime? AdIdIssueDate { get; set; }
    
    public long? AdIdIssueDateHij { get; set; }
    
    public DateTime? AdIdExpiryDate { get; set; }
    
    public long? AdIdExpiryDateHij { get; set; }
    
    public string? AdIqamaNo { get; set; }
    
    public string? AdIqamaIssuedBy { get; set; }
    
    public DateTime? AdIqamaIssueDate { get; set; }
    
    public DateTime? AdIqamaExpiryDate { get; set; }
    
    public string? AdPassportNo { get; set; }
    
    public string? AdPassportTypeId { get; set; }
    
    public string? AdPassportIssuePlace { get; set; }
    
    public DateTime AdPassportIssueDate { get; set; }
    
    public DateTime AdPassportExpiryDate { get; set; }
    
    public string? AdPlaceOfBirth { get; set; }
    
    public DateTime AdDateOfBirth { get; set; }
    
    public int AdDateOfBirthHij { get; set; }
    
    public int AdAgeStageId { get; set; }
    
    public int AdCurrentNationalityId { get; set; }
    
    public int? AdPreviousNationalityId { get; set; }
    
    public int? AdResidenceCountryId { get; set; }
    
    public string? AdEmail { get; set; }
    
    public int AdMobileCountryCode { get; set; }
    
    public string? AdMobileNumber { get; set; }
    
    public string? AdPermitNo { get; set; }
    
    public DateTime? AdPermitDate { get; set; }
    
    public MartialStatusEnum AdMaritalStatusId { get; set; }
    
    public GenderEnum AdGender { get; set; }
    
    public int AdEmbassyId { get; set; }
    
    public string? AdEntryPort { get; set; }
    
    public int AdEntityId { get; set; }
    
    public int? AdAgenciesId { get; set; }
    
    public int AdApplicantTypeId { get; set; }
    
    public int? AdHmMemberTypesId { get; set; }
    
    public bool? AdIsB2C { get; set; }
    
    public bool AdIsGcc { get; set; }
    
    public int? AdCampId { get; set; }
    
    public int? AdSpcCompanyIdMecca { get; set; }
    
    public int? AdSpcCompanyIdMadina { get; set; }
    
    public int State { get; set; }
    
    public DateTime AdPlannedArrivalDate { get; set; }
    
    public DateTime AdPlannedDepartureDate { get; set; }
    
    public string? AdCampSquareNoAr { get; set; }
    
    public string? AdCampSquareNoEn { get; set; }
    
    public string? AdVisaNumber { get; set; }
    
    public string? AdCampGateNo { get; set; }
    
    public int AdCampStreetNo { get; set; }
    
    public int AdGroupNo { get; set; }
    
    public string? AdGroupNameAr { get; set; }
    
    public string? AdGroupNameEn { get; set; }
    
    public string? AdHajVisaPermitStatus { get; set; }
    
    public DateTime Timestamp { get; set; }
    
    public override Individual ToCrmEntity(
        EntityReference? id = null) => ToElmApplicant([], []).ToCrmEntity(id);
    
    public ElmApplicant ToElmApplicant(
        Dictionary<int, Country> countries,
        Dictionary<int, Country> nationalities) => ElmApplicant.Create(this, countries: countries, nationalities);
}
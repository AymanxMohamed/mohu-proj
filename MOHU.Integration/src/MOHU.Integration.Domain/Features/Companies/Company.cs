using Common.Crm.Domain.Common.Factories;
using Common.Crm.Domain.Common.OptionSets.Extensions;
using MOHU.Integration.Domain.Features.Common.ElmReferencedEntities;
using MOHU.Integration.Domain.Features.Companies.Constants;
using MOHU.Integration.Domain.Features.Companies.Enums;

namespace MOHU.Integration.Domain.Features.Companies;

public partial class Company : CrmElmReferencedEntity, IElmReferenceIdResolver
{
    private Company(Entity entity)
        : base(entity)
    {
        SicCode = entity.GetAttributeValue<string>(CompaniesConstants.Fields.SicCode);
        ServiceType = entity.GetEnumValue<ServiceTypeEnum>(CompaniesConstants.Fields.ServiceType);
        OrganizationArabicName = entity.GetAttributeValue<string>(CompaniesConstants.Fields.OrganizationArabicName);
        OrganizationEnglishName = entity.GetAttributeValue<string>(CompaniesConstants.Fields.OrganizationEnglishName);
        LicenseNumber = entity.GetAttributeValue<string>(CompaniesConstants.Fields.LicenseNumber);
        OrganizationCountryId = entity.GetAttributeValue<EntityReference>(CompaniesConstants.Fields.OrganizationCountryId);
        TeamId = entity.GetAttributeValue<EntityReference>(CompaniesConstants.Fields.TeamId);
        IsRepresentativeCompany = entity.GetAttributeValue<bool>(CompaniesConstants.Fields.IsRepresentativeCompany);
        ElmCompanyType = entity.GetEnumValue<ElmCompanyTypeEnum>(CompaniesConstants.Fields.ElmCompanyType);
        Email = entity.GetAttributeValue<string>(CompaniesConstants.Fields.Email);
        Address1Name = entity.GetAttributeValue<string>(CompaniesConstants.Fields.Address1Name);
        Address2Name = entity.GetAttributeValue<string>(CompaniesConstants.Fields.Address2Name);
    }
    
    private Company(
        EntityReference? id, 
        int? elmReferenceId,
        ElmCompanyTypeEnum? elmCompanyType,
        ServiceTypeEnum? serviceType,
        string? organizationArabicName,
        string? organizationEnglishName,
        string? sicCode,
        string? email,
        string? address1Name,
        string? address2Name,
        EntityReference? organizationCountryId = null,
        EntityReference? teamId = null,
        bool? isRepresentativeCompany = null)
        : base(id ?? EntityReferenceFactory.Create(CompaniesConstants.LogicalName), elmReferenceId)
    {
        ElmCompanyType = elmCompanyType;
        ServiceType = serviceType;
        OrganizationEnglishName = organizationEnglishName;
        OrganizationArabicName = organizationArabicName;
        SicCode = sicCode;
        Email = email;
        Address1Name = address1Name;
        Address2Name = address2Name;
        OrganizationCountryId = organizationCountryId;
        TeamId = teamId;
        IsRepresentativeCompany = isRepresentativeCompany;
    }

    public string? SicCode { get; init; }
    
    public string? OrganizationArabicName { get; init; }

    public string? OrganizationEnglishName { get; init; }
    
    public string? LicenseNumber { get; init; }
    
    public string? Email { get; init; }
    
    public string? Address1Name { get; init; }
    
    public string? Address2Name { get; init; }

    public ElmCompanyTypeEnum? ElmCompanyType { get; private set; }
    
    public ServiceTypeEnum? ServiceType { get; private set; }
    
    public EntityReference? OrganizationCountryId { get; init; }
    
    public EntityReference? TeamId { get; init; }
    
    public bool? IsRepresentativeCompany { get; init; }
    
    public static Company Create(Entity entity) => new(entity);

    public static Company Create(
        EntityReference? id, 
        int? elmReferenceId,
        ElmCompanyTypeEnum? elmCompanyType,
        ServiceTypeEnum? serviceType,
        string? organizationArabicName,
        string? organizationEnglishName,
        string? sicCode,
        string? email = null,
        string? address1Name = null,
        string? address2Name = null,
        EntityReference? organizationCountryId = null,
        EntityReference? teamId = null,
        bool? isRepresentativeCompany = null) => new (
        id, 
        elmReferenceId, 
        elmCompanyType, 
        serviceType, 
        organizationArabicName, 
        organizationEnglishName, 
        sicCode, 
        email, 
        address1Name, 
        address2Name, 
        organizationCountryId, 
        teamId, 
        isRepresentativeCompany);

    public void SetTypeInformation(
        ElmCompanyTypeEnum? companyType = null, 
        ServiceTypeEnum? serviceType = null)
    {
        if (companyType != null)
        {
            ElmCompanyType = companyType;
        }
        
        if (serviceType != null)
        {
            ServiceType = serviceType;
        }
    }

    public new Entity ToCrmEntity()
    {
        var entity = base.ToCrmEntity();
        
        entity.EnsureCanCreateFrom(objectToCreate: nameof(Company), CompaniesConstants.LogicalName);
        
        entity.AssignIfNotNull(
            CompaniesConstants.Fields.ServiceType, 
            ServiceType.ToOptionSetValue()
        );
        
        entity.AssignIfNotNull(
            CompaniesConstants.Fields.ElmCompanyType, 
            ElmCompanyType.ToOptionSetValue()
        );
        
        entity.AssignIfNotNull(CompaniesConstants.Fields.SicCode,  SicCode);
        entity.AssignIfNotNull(CompaniesConstants.Fields.OrganizationArabicName,  OrganizationArabicName);
        entity.AssignIfNotNull(CompaniesConstants.Fields.OrganizationEnglishName,  OrganizationEnglishName);
        entity.AssignIfNotNull(CompaniesConstants.Fields.LicenseNumber,  LicenseNumber);
        entity.AssignIfNotNull(CompaniesConstants.Fields.OrganizationCountryId,  OrganizationCountryId);
        
        entity.AssignIfNotNull(CompaniesConstants.Fields.TeamId,  TeamId);
        entity.AssignIfNotNull(CompaniesConstants.Fields.IsRepresentativeCompany,  IsRepresentativeCompany);
        entity.AssignIfNotNull(CompaniesConstants.Fields.Address1Name,  Address1Name);
        entity.AssignIfNotNull(CompaniesConstants.Fields.Address2Name,  Address2Name);
        entity.AssignIfNotNull(CompaniesConstants.Fields.Email,  Email);
        
        return entity;
    }

    public int? ResolveElmReferenceId() => ElmReferenceId;
}
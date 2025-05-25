using Common.Crm.Domain.Common.OptionSets.Extensions;
using Common.Crm.Domain.Common.Utilities.ReflectionUtils;
using ErrorOr;
using Microsoft.Xrm.Sdk.Query;
using MOHU.Integration.Domain.Features.Tickets.Constants;
using MOHU.Integration.Domain.Features.Tickets.Enums;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Linq;

namespace MOHU.Integration.Domain.Features.Tickets.Entities;

public class TicketIntegrationInformation
{
    private static ColumnSet _ticketUpdateColumnSet;

    static TicketIntegrationInformation()
    {
       var columnSet = typeof(TicketsConstants.IntegrationInformation.Fields).GetColumnSet();
       columnSet.AddColumns(typeof(TicketsConstants.BasicInformation.CompanyCheckFields).GetColumnSet().Columns.ToArray());
       columnSet.AddColumn(TicketsConstants.Classification.Fields.Service);
       columnSet.AddColumn(TicketsConstants.BasicInformation.Fields.Title);
       _ticketUpdateColumnSet = columnSet;
    }
    
    private TicketIntegrationInformation(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketIntegrationInformation), TicketsConstants.LogicalName);
        
        ClosureReason = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.IntegrationClosureReason);
        DepartmentClosureReasons = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.DepartmentClosureReasonsComment);
        DepartmentDecision = entity.GetEnumValue<DepartmentDecision>(TicketsConstants.IntegrationInformation.Fields.DepartmentDecision);
        NeedMoreDetailsComment = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.NeedMoreDetailsComment);
        UpdatedBy = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.IntegrationUpdatedBy);
        //ClosureDate = entity.GetAttributeValue<DateTime>(TicketsConstants.IntegrationInformation.Fields.IntegrationClosureDate);
        LastActionDate = entity.GetAttributeValue<DateTime>(TicketsConstants.IntegrationInformation.Fields.IntegrationLastActionDate);
        Comment = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.IntegrationComment);
        IntegrationStatus = entity.GetEnumValue<IntegrationStatus>(TicketsConstants.IntegrationInformation.Fields.IntegrationStatus);
        CompanyPortalUpdated = entity.GetAttributeValue<bool>(TicketsConstants.IntegrationInformation.Fields.IsCompanyPortalUpdated);
        IsNusukPortalUpdated = entity.GetAttributeValue<bool>(TicketsConstants.IntegrationInformation.Fields.IsNusukPortalUpdated);
        CompanyServiceNeedMoreInformation = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.CompanyServiceNeedMoreInformation);
        CompanyServiceDecisionCode = entity.GetEnumValue<CompanyServiceDecisionEnum>(TicketsConstants.IntegrationInformation.Fields.CompanyServiceDecisionCode);
    }

    private TicketIntegrationInformation(
        string? closureReason, 
        DateTime? closureDate,
        IntegrationStatus? integrationStatus, 
        string? comment, 
        string? updatedBy,
        DateTime? lastActionDate)
    {
        ClosureReason = closureReason;
        //ClosureDate = closureDate;
        IntegrationStatus = integrationStatus;
        DepartmentDecision = integrationStatus.ToEnum<IntegrationStatus, DepartmentDecision>();
        Comment = comment;
        UpdatedBy = updatedBy;
        LastActionDate = lastActionDate;
    }
    
    public static ColumnSet TicketUpdateColumnSet => _ticketUpdateColumnSet;
    
    public bool? IsNusukPortalUpdated { get; private set; }

    public string? CompanyServiceNeedMoreInformation { get; private set; }

    public CompanyServiceDecisionEnum? CompanyServiceDecisionCode { get; private set; }

    public string? ClosureReason { get; private set; }
    
    public string? NeedMoreDetailsComment { get; private set; }
    public string? DepartmentClosureReasons { get; private set; }
    public DepartmentDecision? DepartmentDecision { get; private set; }

  //  public DateTime? ClosureDate { get; private set; }

    public IntegrationStatus? IntegrationStatus { get; private set; }

    public string? Comment { get; private set; }

    public string? UpdatedBy { get; private set; }
    
    public bool? CompanyPortalUpdated { get; private set; }

    public DateTime? LastActionDate { get; private set; }

    private void xxxxxxxxxxxxxxxxxxxxx(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketIntegrationInformation), TicketsConstants.LogicalName);

        ClosureReason = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.IntegrationClosureReason);
        DepartmentClosureReasons = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.DepartmentClosureReasonsComment);
        DepartmentDecision = entity.GetEnumValue<DepartmentDecision>(TicketsConstants.IntegrationInformation.Fields.DepartmentDecision);
        NeedMoreDetailsComment = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.NeedMoreDetailsComment);
        UpdatedBy = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.IntegrationUpdatedBy);
        //ClosureDate = entity.GetAttributeValue<DateTime>(TicketsConstants.IntegrationInformation.Fields.IntegrationClosureDate);
        LastActionDate = entity.GetAttributeValue<DateTime>(TicketsConstants.IntegrationInformation.Fields.IntegrationLastActionDate);
        Comment = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.IntegrationComment);
        IntegrationStatus = entity.GetEnumValue<IntegrationStatus>(TicketsConstants.IntegrationInformation.Fields.IntegrationStatus);
        CompanyPortalUpdated = entity.GetAttributeValue<bool>(TicketsConstants.IntegrationInformation.Fields.IsCompanyPortalUpdated);
        IsNusukPortalUpdated = entity.GetAttributeValue<bool>(TicketsConstants.IntegrationInformation.Fields.IsNusukPortalUpdated);
        CompanyServiceNeedMoreInformation = entity.GetAttributeValue<string>(TicketsConstants.IntegrationInformation.Fields.CompanyServiceNeedMoreInformation);
        CompanyServiceDecisionCode = entity.GetEnumValue<CompanyServiceDecisionEnum>(TicketsConstants.IntegrationInformation.Fields.CompanyServiceDecisionCode);
    }

    public void Update(string comment, string updatedBy, IntegrationStatus integrationStatus,Guid serviceId)
    {
        IntegrationStatus = integrationStatus;
        //External ticketNumber ??

        if (serviceId == new Guid("7a8c2cbc-8f8c-ef11-ac20-6045bd8fae55") // Mentoring // lost 
               || serviceId == new Guid("f1cc8dda-8f8c-ef11-ac20-6045bd8fae55") // missing
               || serviceId == new Guid("f8921d0b-4233-f011-8c4d-7c1e522940f7") // sos
               )//nusuk
        {
            IsNusukPortalUpdated = true;
            CompanyServiceDecisionCode = integrationStatus.ToEnum<IntegrationStatus, CompanyServiceDecisionEnum>();
            CompanyServiceNeedMoreInformation = comment;

        }
        else if (serviceId == new Guid("7b80a868-2dcc-ee11-907a-6045bd8c92a2") //haj
            || serviceId == new Guid("7980a868-2dcc-ee11-907a-6045bd8c92a2") //umrah
            )
        {
            CompanyPortalUpdated = true;
            DepartmentDecision = integrationStatus.ToEnum<IntegrationStatus, DepartmentDecision>();
            DepartmentClosureReasons = comment;
            ClosureReason = comment;
            NeedMoreDetailsComment = comment;

        }

         ///// if (integrationStatus == Enums.IntegrationStatus.NeedMoreDetails)
        {
            LastActionDate = DateTime.UtcNow;
            UpdatedBy = updatedBy;
            //ClosureDate = DateTime.UtcNow;
            //NeedMoreDetailsComment = comment;

        }
    }


    private void Update(string comment, string updatedBy, IntegrationStatus integrationStatus)
    {
        LastActionDate = DateTime.UtcNow;
        UpdatedBy = updatedBy;
        NeedMoreDetailsComment = comment;

        // if (service= nusuk 3naya)
        IsNusukPortalUpdated = true;
        DepartmentDecision = integrationStatus.ToEnum<IntegrationStatus, DepartmentDecision>();

        //else if(service= company)
        CompanyPortalUpdated = true;
        CompanyServiceDecisionCode = integrationStatus.ToEnum<IntegrationStatus, CompanyServiceDecisionEnum>();
        CompanyServiceNeedMoreInformation = comment;
    }

    public static TicketIntegrationInformation Create(Entity entity) => new(entity);

    public static TicketIntegrationInformation Create(
        string comment, 
        string updatedBy,
        IntegrationStatus integrationStatus) => 
        new(
            comment, 
            DateTime.UtcNow, 
            integrationStatus, 
            comment, updatedBy,
            DateTime.UtcNow);

    public static TicketIntegrationInformation Create(
        string? closureReason,
        DateTime? closureDate,
        IntegrationStatus? integrationStatus,
        string? comment,
        string? updatedBy,
        DateTime? lastActionDate) =>
        new(closureReason, closureDate, integrationStatus, comment, updatedBy, lastActionDate);

    internal void UpdateEntity(Entity entity)
    {
        entity.EnsureCanCreateFrom(objectToCreate: nameof(TicketIntegrationInformation), TicketsConstants.LogicalName);
        
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.NeedMoreDetailsComment, ClosureReason);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.DepartmentClosureReasonsComment, ClosureReason);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.DepartmentDecision, DepartmentDecision.ToOptionSetValue());

        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IntegrationClosureReason, ClosureReason);
        //entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IntegrationClosureDate, ClosureDate);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IntegrationComment, Comment);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IntegrationLastActionDate, LastActionDate);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IntegrationUpdatedBy, UpdatedBy);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IsCompanyPortalUpdated, CompanyPortalUpdated);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IsNusukPortalUpdated, IsNusukPortalUpdated);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.CompanyServiceNeedMoreInformation, CompanyServiceNeedMoreInformation);
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.IntegrationStatus, IntegrationStatus.ToOptionSetValue());
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.CompanyServiceDecisionCode, CompanyServiceDecisionCode.ToOptionSetValue());
        entity.AssignIfNotNull(TicketsConstants.IntegrationInformation.Fields.CompanyServiceDecisionCode, CompanyServiceDecisionCode.ToOptionSetValue());
    }
}
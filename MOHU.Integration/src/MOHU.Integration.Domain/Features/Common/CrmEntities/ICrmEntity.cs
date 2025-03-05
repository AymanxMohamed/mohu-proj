namespace MOHU.Integration.Domain.Features.Common.CrmEntities;

public interface ICrmEntity
{
    public EntityReference Id { get; }

    Entity ToCrmEntity();
}
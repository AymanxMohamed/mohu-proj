using Common.Crm.Domain.Common.ColumnSets.Constants;
using Common.Crm.Domain.Common.Extensions;
using Common.Crm.Domain.Common.Factories;
using Common.Crm.Domain.Common.OptionSets.Constants;
using Common.Crm.Infrastructure.Common.Extensions;
using Common.Crm.Infrastructure.Repositories.Interfaces;

namespace Common.Crm.Infrastructure.Repositories.Concretes;

public class GenericRepository(IOrganizationService organizationService) : IGenericRepository
{
    public readonly IOrganizationService OrganizationService = organizationService;

    public void Create(Entity entity)
    {
        UnitOfWork.Requests.Add(RequestsFactory.CreateCreateRequest(entity));
    }
        
    public void CreateMany(
        string entityLogicalName, 
        IEnumerable<Entity> relatedEntities,
        string relatedEntityLookupNameOnRequiredEntity,
        Dictionary<string, object>? attributes = null)
    {
        attributes = attributes ?? new Dictionary<string, object>();
            
        foreach (var entity in relatedEntities)
        {
            if (!attributes.ContainsKey(relatedEntityLookupNameOnRequiredEntity))
            {
                attributes.Add(
                    relatedEntityLookupNameOnRequiredEntity, 
                    entity.ToEntityReference()); 
            }
            else
            {
                attributes[relatedEntityLookupNameOnRequiredEntity] = entity.ToEntityReference();
            }

            Create(EntityFactory.CreateNew(entityLogicalName, attributes: attributes));
        }
    }
        
    // public void Create<T>(T type)
    // {
    //     UnitOfWork.Requests.Add(RequestsFactory.CreateCreateRequest(CrmMapper.ConvertToCRMEntity(type)));
    // }
        
    public void CreateMany(IEnumerable<Entity> entities)
    {
        UnitOfWork.Requests.AddRange(entities.Select(RequestsFactory.CreateCreateRequest));
    }
        
    // public void CreateMany<T>(IEnumerable<T> types)
    // {
    //     UnitOfWork.Requests.AddRange(types.Select(type => 
    //         RequestsFactory.CreateCreateRequest(CrmMapper.ConvertToCRMEntity(type))));
    // }
        
    public IEnumerable<Entity> ListAll(QueryBase query)
    {
        var entityCollection = OrganizationService.RetrieveMultiple(query);
        return entityCollection.Entities;
    }
        
    public IEnumerable<Entity> ListAll(QueryExpression query)
    {
        return ListAll(query as QueryBase);
    }

    // public IEnumerable<T> ListAll<T>(QueryBase query)
    // {
    //     var entityCollection = _organizationService.RetrieveMultiple(query);
    //     return entityCollection.Entities.Select(CrmMapper.ConvertToT<T>);
    // }
        
    // public IEnumerable<T> ListAll<T>(QueryExpression query)
    // {
    //     return ListAll<T>(query as QueryBase);
    // }
        
    public IEnumerable<Entity> ListAllFilteredByEntityReference(
        string logicalName,
        EntityReference entityReference,
        string lookupLogicalName,
        ColumnSet? columnSet = null)
    {
        var query = QueryExpressionFactory.CreateQueryExpression(
            entityLogicalName: logicalName,
            columnSet: columnSet ?? ColumnSetConstants.AllColumns,
            conditionExpressions: new [] { entityReference.GetFilterByConditionExpression(lookupLogicalName) } );

        return ListAll(query);
    }
        
    public Entity GetByEntityReference(EntityReference entityReference, ColumnSet? columnSet = null)
    {
        return OrganizationService.Retrieve(
            entityReference.LogicalName, 
            entityReference.Id, 
            columnSet ?? new ColumnSet(true));
    }
        
    // public T GetByEntityReference<T>(EntityReference entityReference, ColumnSet columnSet = null)
    // {
    //     return _organizationService.Retrieve(
    //             entityReference.LogicalName, 
    //             entityReference.Id, 
    //             columnSet ?? new ColumnSet(true)).
    //         ConvertToT<T>();
    // }
    
    public Entity GetById(string entityLogicalName, Guid id, ColumnSet? columnSet = null)
    {
        return GetByEntityReference(new EntityReference(entityLogicalName, id), columnSet);
    }
        
    // public T GetById<T>(string entityLogicalName, Guid id, ColumnSet columnSet = null)
    // {
    //     return GetByEntityReference<T>(new EntityReference(entityLogicalName, id), columnSet);
    // }

    public void Update(Entity entity)
    {
        UnitOfWork.Requests.Add(RequestsFactory.CreateUpdateRequest(entity));
    }
        
    public void UpdateMany(IEnumerable<Entity> entities)
    {
        UnitOfWork.Requests.AddRange(entities.Select(RequestsFactory.CreateUpdateRequest));
    }

    // public void Update<T>(T type)
    // {
    //     UnitOfWork.Requests.Add(RequestsFactory.CreateUpdateRequest(CrmMapper.ConvertToCRMEntity(type)));
    // }
        
    // public void UpdateMany<T>(IEnumerable<T> types)
    // {
    //     UnitOfWork.Requests.AddRange(types.Select(type => 
    //         RequestsFactory.CreateUpdateRequest(CrmMapper.ConvertToCRMEntity(type))));
    // }

    public void Delete(Entity entity)
    {
        UnitOfWork.Requests.Add(RequestsFactory.CreateDeleteRequest(entity));
    }
        
    public void DeleteMany(IEnumerable<Entity> entities)
    {
        UnitOfWork.Requests.AddRange(entities.Select(RequestsFactory.CreateDeleteRequest));
    }

    public void DeleteAllRecordsRelatedToEntity(
        string logicalName,
        EntityReference entityReference,
        string lookupLogicalName)
    {
        DeleteMany(entities: ListAllFilteredByEntityReference(
            logicalName,
            entityReference,
            lookupLogicalName));
    }

    // public void Delete<T>(T type)
    // {
    //     UnitOfWork.Requests.Add(RequestsFactory.CreateDeleteRequest(CrmMapper.ConvertToCRMEntity(type)));
    // }
        
        
    // public void DeleteMany<T>(IEnumerable<T> types)
    // {
    //     UnitOfWork.Requests.AddRange(types.Select(type => 
    //         RequestsFactory.CreateDeleteRequest(CrmMapper.ConvertToCRMEntity(type))));
    // }

    public void Deactivate(Entity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        if (!entity.Contains(OptionSetConstants.Status.FieldLogicalName))
        {
            entity = GetById(
                entity.LogicalName, 
                entity.ToEntityReference().Id,
                ColumnSetConstants.SystemStatusesColumns);
        }

        if (entity.IsDeactivated()) return;
    
        UnitOfWork.Requests.Add(RequestsFactory.CreateSetStateRequest(
            entity,
            status: OptionSetConstants.Status.InActive,
            statusReason: OptionSetConstants.StatusReason.InActive));
    }

    public void Commit()
    {
        UnitOfWork.ExecuteRequests(OrganizationService);
    }
}
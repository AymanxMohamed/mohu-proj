using Common.Crm.Infrastructure.Common.Extensions;
using MOHU.Integration.Contracts.Dto.CreateProfile;
using MOHU.Integration.Contracts.Services;

namespace MOHU.Integration.Application.Service
{
    public class UserService : IUserService
    {
        private readonly ICrmContext _context;
        public UserService(ICrmContext context)
        {
            _context = context;
        }
        public async Task<LookupDto> GetUserByUsernameAsync(string username)
        {
            LookupDto result = null;
            var query = new QueryExpression(SystemUser.EntityLogicalName)
            {
                NoLock = true,
                ColumnSet = new ColumnSet(SystemUser.Fields.DomainName)
            };
            query.Criteria.AddCondition(SystemUser.Fields.DomainName, ConditionOperator.Equal, username);
            var userCollection = await _context.ServiceClient.RetrieveMultipleAsync(query);

            if (userCollection != null && userCollection.Entities.Count > 0)
            {
                result = new LookupDto
                {
                    Id = userCollection.Entities.FirstOrDefault().Id,
                    EntityLogicalName = userCollection.EntityName

                };
            }
            return result;
        }


        public async Task<List<UserWithRolesDto>> GetUserRolesAsync()
        {
            var query = new QueryExpression("systemuser")
            {
                ColumnSet = new ColumnSet("fullname", "systemuserid", "internalemailaddress"),
                Orders = { new OrderExpression("fullname", OrderType.Ascending) },
                LinkEntities =
                {
                    // Direct role assignments
                    new LinkEntity("systemuser", "systemuserroles", "systemuserid", "systemuserid", JoinOperator.LeftOuter)
                    {
                        EntityAlias = "directroles",
                        LinkEntities =
                        {
                            new LinkEntity("systemuserroles", "role", "roleid", "roleid", JoinOperator.Inner)
                            {
                                EntityAlias = "directrole",
                                Columns = new ColumnSet("name")
                            }
                        }
                    },
                    // Team role assignments
                    new LinkEntity("systemuser", "teammembership", "systemuserid", "systemuserid", JoinOperator.LeftOuter)
                    {
                        EntityAlias = "teammembership",
                        LinkEntities =
                        {
                            new LinkEntity("teammembership", "teamroles", "teamid", "teamid", JoinOperator.Inner)
                            {
                                EntityAlias = "teamroles",
                                LinkEntities =
                                {
                                    new LinkEntity("teamroles", "role", "roleid", "roleid", JoinOperator.Inner)
                                    {
                                        EntityAlias = "teamrole",
                                        Columns = new ColumnSet("name")
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var rawData = await _context.ServiceClient.RetrieveMultipleAsync(query);

            var usersDict = new Dictionary<Guid, UserWithRolesDto>();

            foreach (var entity in rawData?.Entities ?? Enumerable.Empty<Entity>())
            {
                Guid userId = entity.GetAttributeValue<Guid>("systemuserid");
                string fullName = entity.GetAttributeValue<string>("fullname");
                string primaryEmail = entity.GetAttributeValue<string>("internalemailaddress");

                if (!usersDict.TryGetValue(userId, out var userDto))
                {
                    userDto = new UserWithRolesDto
                    {
                        SystemUserId = userId,
                        FullName = fullName,
                        PrimaryEmail = primaryEmail,
                        AllRoles = new HashSet<string>() // Using HashSet to automatically avoid duplicates
                    };
                    usersDict[userId] = userDto;
                }

                // Add direct role if exists
                var directRole = entity.GetAttributeValue<AliasedValue>("directrole.name")?.Value as string;
                if (!string.IsNullOrEmpty(directRole))
                {
                    userDto.AllRoles.Add(directRole);
                }

                // Add team role if exists
                var teamRole = entity.GetAttributeValue<AliasedValue>("teamrole.name")?.Value as string;
                if (!string.IsNullOrEmpty(teamRole))
                {
                    userDto.AllRoles.Add(teamRole);
                }
            }

            return usersDict.Values.ToList();
        }

      
    }
}

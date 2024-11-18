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
    }
}

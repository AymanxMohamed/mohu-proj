namespace MOHU.Integration.Contracts.Dto.Common
{
    public class LookupDto
    {
        public Guid Id { get; set; }
        public string EntityLogicalName { get; set; }
        public LookupDto() { }
        public LookupDto(Guid id, string entityLogicalName)
        {
            Id = id;
            EntityLogicalName = entityLogicalName;
        }
    }
}

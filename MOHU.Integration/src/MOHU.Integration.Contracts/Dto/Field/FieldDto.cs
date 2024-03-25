namespace MOHU.Integration.Contracts.Dto.Field
{
    public class FieldDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool Mandatory { get; set; }
        public string Regex { get; set; }
        public string RegexErrorMessage { get; set; }
        public int PortalDisplayOrder { get; set; }
        public List<FieldValueDto> Values { get; set; }
    }
}

namespace MOHU.Integration.Contracts.Dto.Common
{
    public class OptionDto
    {
        public string Name { get; set; }
        public int? Value { get; set; }
        public OptionDto() { }
        public OptionDto(int value, string name)
        {
            Value = value;
            Name = name;
        }
    }
}

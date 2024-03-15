using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Dto.Common
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

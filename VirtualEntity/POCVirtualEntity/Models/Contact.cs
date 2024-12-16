using System.ComponentModel.DataAnnotations;

namespace POCVirtualEntity.Models
{
    public class Contact
    {
        [Required]
        public string ContactId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}

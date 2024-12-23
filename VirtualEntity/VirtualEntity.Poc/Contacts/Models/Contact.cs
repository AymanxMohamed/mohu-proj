using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VirtualEntity.Poc.Contacts.Models;

public class Contact
{
    // ReSharper disable once UnusedMember.Local
    public Contact()
    {
        
    }

    private Contact(Guid contactId, string fullName, string email, string phoneNumber)
    {
        ContactId = contactId;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    
    [Required] public Guid ContactId { get; set; }
    
    public string? FullName { get; set; }
    
    public string? Email { get; set; }
    
    public string? PhoneNumber { get; set; }
    

    public static Contact Create(string? contactId, string fullName, string email, string phoneNumber)
    {
        if (!Guid.TryParse(contactId, out var id))
        {
            throw new ArgumentException("Invalid contact id", nameof(contactId));
        }

        return new Contact(id, fullName, email, phoneNumber);
    }


}
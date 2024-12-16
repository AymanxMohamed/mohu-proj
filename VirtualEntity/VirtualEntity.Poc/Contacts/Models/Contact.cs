using System.ComponentModel.DataAnnotations;

namespace VirtualEntity.Poc.Contacts.Models;

public class Contact
{
    // ReSharper disable once UnusedMember.Local
    private Contact()
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
    public string FullName { get; set; }  = null!;
    public string Email { get; set; }  = null!;
    public string PhoneNumber { get; set; }  = null!;

    public static Contact Create(string? contactId, string? fullName, string? email, string? phoneNumber)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(contactId, nameof(contactId));
        ArgumentException.ThrowIfNullOrWhiteSpace(fullName, nameof(fullName));
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
        ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber, nameof(phoneNumber));
        
        if (!Guid.TryParse(contactId, out var id))
        {
            throw new ArgumentException("Invalid contact id", nameof(contactId));
        }

        return new Contact(id, fullName, email, phoneNumber);
    }


}
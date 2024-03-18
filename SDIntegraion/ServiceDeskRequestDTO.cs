using System.ComponentModel.DataAnnotations;

namespace SDIntegraion
{
    public class ServiceDeskRequestDTO
    {
        public string AffectedService { get; set; } 
        public string PassportNumber { get; set; } 
        public string PhoneNumber { get; set; } 
        public string CRMNumber { get; set; } 
        public string ServiceRecipient { get; set; } 
        public string Contact { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string? UserType { get; set; }
        public string Nationality { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public object? VisaNumber { get; set; } 
        public object? MobileType { get; set; }   
        public object? ReservationNumber { get; set; }  
        public object? ErrorCode { get; set; } 
        public object? LoginMethod { get; set; }  
        public string Title { get; set; } 
        public string Description { get; set; } 
        public string Category { get; set; } 
        public string Status { get; set; } 
        public string Subcategory { get; set; } 
        public string Area { get; set; } 
        public string Impact { get; set; } 
        public string Urgency { get; set; } 
        public string AssignmentGroup { get; set; } 

    }
}

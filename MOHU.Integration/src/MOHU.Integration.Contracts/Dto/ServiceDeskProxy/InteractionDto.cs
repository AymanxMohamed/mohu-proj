using Newtonsoft.Json;

namespace SDIntegraion
{
    public class InteractionDto
    {
        [JsonProperty("AffectedService")]
        public string AffectedService { get; set; }
        [JsonProperty("PassportNumber")]
        public string PassportNumber { get; set; }
        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("CRMNumber")]

        public string CRMNumber { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [JsonProperty("Nationality")]
        public string Nationality { get; set; }
        [JsonProperty("Title")]

        public string Title { get; set; }
        [JsonProperty("Description")]

        public string Description { get; set; }
        [JsonProperty("Subcategory")]

        public string Subcategory { get; set; }
        [JsonProperty("Area")]

        public string Area { get; set; }
        [JsonProperty("ServiceRecipient")]

        public string ServiceRecipient { get; set; }
        [JsonProperty("Contact")]

        public string Contact { get; set; }
        [JsonProperty("Category")]

        public string Category { get; set; }
        [JsonProperty("Status")]

        public string Status { get; set; }
        [JsonProperty("AssignmentGroup")]

        public string AssignmentGroup { get; set; }
        [JsonProperty("Impact")]

        public string Impact { get; set; }
        [JsonProperty("Urgency")]

        public string Urgency { get; set; }
        [JsonProperty("UserType")]

        public object? UserType { get; set; }
        [JsonProperty("DateOfBirth")]

        public object? DateOfBirth { get; set; }
        [JsonProperty("VisaNumber")]

        public object? VisaNumber { get; set; }
        [JsonProperty("MobileType")]

        public object? MobileType { get; set; }
        [JsonProperty("ReservationNumber")]

        public object? ReservationNumber { get; set; }
        [JsonProperty("ErrorCode")]

        public object? ErrorCode { get; set; }
        [JsonProperty("LoginMethod")]

        public object? LoginMethod { get; set; }

    }
}

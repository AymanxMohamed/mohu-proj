using System.ComponentModel.DataAnnotations;

namespace MOHU.Integration.Contracts.Dto
{
    public class UpdateStatusRequest
    {

        public Guid CustomerId { get; set; }

        public Guid TicketId { get; set; }

        [MaxLength(400)]
        public string Resolution { get; set; }


        public DateTime? ResolutionDate { get; set; }

        public IntegrationStatus IntegrationStatus { get; set; }

    }
}

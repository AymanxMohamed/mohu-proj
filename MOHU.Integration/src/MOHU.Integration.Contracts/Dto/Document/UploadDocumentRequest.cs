namespace MOHU.Integration.Contracts.Dto.Document
{
    public class UploadDocumentRequest
    {
        public Guid TicketId { get; set; }
        public ICollection<UploadDocumentContentDto> Documents { get; set; }
    }
}

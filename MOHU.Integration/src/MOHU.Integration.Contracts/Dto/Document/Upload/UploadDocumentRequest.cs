using MOHU.Integration.Contracts.Dto.Document.Download;

namespace MOHU.Integration.Contracts.Dto.Document.Upload
{
    public class UploadDocumentRequest
    {
        public Guid TicketId { get; set; }
        public ICollection<UploadDocumentContentDto> Documents { get; set; }
    }
}

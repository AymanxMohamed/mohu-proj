namespace MOHU.Integration.Contracts.Dto.Document.Upload
{
    public class UploadDocumentResponse
    {
        public ICollection<DocumentDto> UploadedDocuments { get; set; }
        public ICollection<FailedDocumentUploadDto> FailedDocuments { get; set; }
        public UploadDocumentResponse()
        {
            UploadedDocuments = new List<DocumentDto>();
            FailedDocuments = new List<FailedDocumentUploadDto>();
        }
    }
}

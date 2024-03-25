using MOHU.Integration.Contracts.Dto.Document.Download;
using MOHU.Integration.Contracts.Dto.Document.List;
using MOHU.Integration.Contracts.Dto.Document.Upload;

namespace MOHU.Integration.Contracts.Interface
{
    public interface IDocumentService
    {
        Task<UploadDocumentResponse> UploadDocumentAsync(List<UploadDocumentContentDto> documents, Guid ticketId);
        Task<DownloadDocumentResponse> DownloadAttachmentAsync(string filePath, Guid ticketId);
        Task<GetFilesInFolderResponse> GetFilesInFolderAsync(string ticketId);
    }
}
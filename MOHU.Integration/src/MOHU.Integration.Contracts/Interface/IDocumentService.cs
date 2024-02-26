namespace MOHU.Integration.Contracts.Interface
{
    public interface IDocumentService
    {
        Task Initialize();
        Task<(Stream fileStream, string fileName)> DownloadAttachmentByFileIdAsync(string fileId, string libraryName);    }
}

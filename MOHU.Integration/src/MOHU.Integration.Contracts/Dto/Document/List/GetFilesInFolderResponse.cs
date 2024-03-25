using MOHU.Integration.Contracts.Dto.Field;

namespace MOHU.Integration.Contracts.Dto.Document.List
{
    public class GetFilesInFolderResponse
    {
        public List<FileDto> Files { get; set; }
        public GetFilesInFolderResponse()
        {
            Files = new List<FileDto>();
        }
    }
}

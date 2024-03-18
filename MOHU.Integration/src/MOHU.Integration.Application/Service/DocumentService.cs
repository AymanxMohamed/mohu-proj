using MOHU.Integration.Application.Exceptions;
using MOHU.Integration.Contracts.Dto.Document;
using MOHU.Integration.Contracts.Dto.Document.Download;
using MOHU.Integration.Contracts.Dto.Document.List;
using MOHU.Integration.Contracts.Dto.Document.Upload;
using MOHU.Integration.Contracts.Interface;
using MOHU.Integration.Contracts.Interface.Common;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MOHU.Integration.Application.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationService _configurationService;
        public DocumentService(IHttpClientFactory httpClientFactory, IConfigurationService configurationService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configurationService = configurationService;
        }

        public async Task<DownloadDocumentResponse> DownloadAttachmentAsync(string filePath, Guid ticketId)
        {
            var downloadResult = await DownloadAsync(filePath);
            if (downloadResult is not null)
                return new DownloadDocumentResponse { Content = downloadResult.Content.FileContent, Name = downloadResult.Name, ContentType = downloadResult.Content.ContentType };
            
            throw new NotFoundException("File not found");
        }

        public async Task<UploadDocumentResponse> UploadDocumentAsync(List<UploadDocumentContentDto> documents, Guid ticketId)
        {
            var result = new UploadDocumentResponse();

            foreach (var document in documents)
            {
                if (!IsValidExtension(document.Name))
                {
                    result.FailedDocuments.Add(new FailedDocumentUploadDto { Name = document.Name, Error = "Extension not allowed" });
                    continue;
                }
                if (!IsValidSize())
                {
                    result.FailedDocuments.Add(new FailedDocumentUploadDto { Name = document.Name, Error = "File size is too big" });
                    continue;
                }
                var uploadResult = await UploadAsync(document, ticketId);
                if (uploadResult.isUploaded)
                    result.UploadedDocuments.Add(new DocumentDto { Id = uploadResult.filePathOrErrorMessage, Name = document.Name });
            }
            return result;
        }
        public async Task<GetFilesInFolderResponse> GetFilesInFolderAsync(string ticketId)
        {
            var result = new GetFilesInFolderResponse();
            var url = await _configurationService.GetConfigurationValueAsync("GetFilesInFolderFlowUrl");
            using StringContent jsonContent = new(JsonSerializer.Serialize(new { ticketId }),
        Encoding.UTF8,
        "application/json");

            var response = await _httpClient.PostAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<GetFilesInFolderResponse>(content);
            }
            return result;
        }

        private bool IsValidExtension(string fileName)
        {
            return true;
        }
        private bool IsValidSize()
        {
            return true;
        }
        private async Task<(bool isUploaded,string filePathOrErrorMessage)> UploadAsync(UploadDocumentContentDto documentDto, Guid ticketId)
        {
            var url = await _configurationService.GetConfigurationValueAsync("UploadDocumentFlowUrl");
          using StringContent jsonContent = new ( 
              JsonSerializer.Serialize(new {FileName = documentDto.Name,FileContent =
              documentDto.Bytes,TicketId= ticketId}),
         Encoding.UTF8,
         "application/json");

         var response = await _httpClient.PostAsync(url,jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var flowResponse = await response.Content.ReadFromJsonAsync<DocumentFlowResponse>();
                return (true,flowResponse.Id);
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return (false,jsonResponse);
        }
        private async Task<DownloadDocumentFlowResponse> DownloadAsync(string filePath)
        {
            var result = new DownloadDocumentFlowResponse();
            var url = await _configurationService.GetConfigurationValueAsync("DownloadDocumentFlowUrl");
            using StringContent jsonContent = new(JsonSerializer.Serialize(new { FileId = filePath }),
           Encoding.UTF8,
           "application/json");

            var response = await _httpClient.PostAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<DownloadDocumentFlowResponse>(content);
            }
            return result;
        }

       
    }
}

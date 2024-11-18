using MOHU.Integration.Contracts.Dto.Document;
using MOHU.Integration.Contracts.Dto.Document.Download;
using MOHU.Integration.Contracts.Dto.Document.List;
using MOHU.Integration.Contracts.Dto.Document.Upload;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MOHU.Integration.Application.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationService _configurationService;
        private readonly ICrmContext _crmContext;
        private readonly IStringLocalizer _localizer;
        public DocumentService(IHttpClientFactory httpClientFactory, IConfigurationService configurationService, ICrmContext crmContext, IStringLocalizer localizer)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configurationService = configurationService;
            _crmContext = crmContext;
            _localizer = localizer;
        }

        public async Task<DownloadDocumentResponse> DownloadAttachmentAsync(string filePath, Guid ticketId)
        {
            var downloadResult = await DownloadAsync(filePath);
            if (downloadResult is not null)
                return new DownloadDocumentResponse { Content = downloadResult?.Content.FileContent, Name = downloadResult?.Name, ContentType = downloadResult?.Content.ContentType };
            
            throw new NotFoundException("File not found");
        }

        public async Task<UploadDocumentResponse> UploadDocumentAsync(List<UploadDocumentContentDto> documents, Guid ticketId)
        {
            var result = new UploadDocumentResponse();
            if (documents.Count == 0)
                throw new BadRequestException(_localizer[ErrorMessageCodes.NoFilesFound]);
            foreach (var document in documents)
            {
                if (!await IsValidExtension(document.ContentType.Split('/')?.LastOrDefault()))
                {
                    result.FailedDocuments.Add(new FailedDocumentUploadDto { Name = document.Name, Error = "Extension not allowed" });
                    continue;
                }
                if (!await IsValidSize(document.Size))
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

        private async Task<bool> IsValidExtension(string fileExtension)
        {
            var documentSettings = await GetDocumentSettingsAsync();
            var allowedExtensions = documentSettings.GetAttributeValue<string>(ldv_documentsetting.Fields.ldv_allowedextensions).Split(',');
            return allowedExtensions.Contains(fileExtension.ToLower());
        }
        private async Task<bool> IsValidSize(float fileSizeInKb)
        {
            var documentSettings = await GetDocumentSettingsAsync();
            var maxFileSize = documentSettings.GetAttributeValue<int>(ldv_documentsetting.Fields.ldv_allowedsizeinkb);
            if (fileSizeInKb > maxFileSize)
                throw new BadRequestException($"File size {fileSizeInKb} is greater than allowed size ({maxFileSize}).");
            return true;
        }
        private async Task<Entity> GetDocumentSettingsAsync()
        {
            var documentSettingId = Guid.Parse(await _configurationService.GetConfigurationValueAsync("DefaultDocumentSettings"));
            var documentSettings = await _crmContext.ServiceClient.RetrieveAsync(ldv_documentsetting.EntityLogicalName, documentSettingId, 
                new Microsoft.Xrm.Sdk.Query.ColumnSet(ldv_documentsetting.Fields.ldv_allowedextensions,ldv_documentsetting.Fields.ldv_allowedsizeinkb));
            return documentSettings;

        }
        private async Task<(bool isUploaded,string filePathOrErrorMessage)> UploadAsync(UploadDocumentContentDto documentDto, Guid ticketId)
        {
            var url = await _configurationService.GetConfigurationValueAsync("UploadDocumentFlowUrl");
          using StringContent jsonContent = new ( 
              JsonSerializer.Serialize(new {FileName = documentDto.Name,FileContent =
              documentDto.Content,TicketId= ticketId}),
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
            DownloadDocumentFlowResponse result = null;
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

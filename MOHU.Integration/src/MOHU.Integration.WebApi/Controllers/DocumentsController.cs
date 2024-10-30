using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.Document.Download;
using MOHU.Integration.Contracts.Dto.Document.Upload;
using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController(IDocumentService documentService) : BaseController
    {
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<DownloadDocumentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<DownloadDocumentResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<DownloadDocumentResponse>), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ResponseMessage<DownloadDocumentResponse>> Get(string documentId, Guid ticketId)
        {
            var result = await documentService.DownloadAttachmentAsync(documentId, ticketId);
            return Ok(result);
        }
        [ProducesResponseType(typeof(ResponseMessage<UploadDocumentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<UploadDocumentResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<UploadDocumentResponse>), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ResponseMessage<UploadDocumentResponse>> Post([FromForm]IFormFileCollection files, Guid ticketId)
        {
            var documentsToUpload = new List<UploadDocumentContentDto>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using var ms = new MemoryStream();
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string base64 = Convert.ToBase64String(fileBytes);
                    documentsToUpload.Add(new UploadDocumentContentDto
                    {
                        Content = $"data:{file.ContentType};base64,{base64}",
                        Name = file.FileName,
                        Size = file.Length/1024f,
                        ContentType = file.ContentType
                    });
                }
            }
            var result = await documentService.UploadDocumentAsync(documentsToUpload, ticketId);
            return Ok(result);
        }

    }

}

using Microsoft.AspNetCore.Mvc;
using MOHU.Integration.Contracts.Dto.Common;
using MOHU.Integration.Contracts.Dto.Document;

namespace MOHU.Integration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage<DownloadAttachmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<DownloadAttachmentResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<DownloadAttachmentResponse>), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ResponseMessage<DownloadAttachmentResponse>> Get(Guid attachmentId, Guid ticketId)
        {
            return new ResponseMessage<DownloadAttachmentResponse> { };
        }
        [ProducesResponseType(typeof(ResponseMessage<UploadDocumentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage<UploadDocumentResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage<UploadDocumentResponse>), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ResponseMessage<UploadDocumentResponse>> Post([FromForm]IFormFileCollection files, Guid ticketId)
        {
          
            return new ResponseMessage<UploadDocumentResponse> { Result = new UploadDocumentResponse { } };
        }
    }
}

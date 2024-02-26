using Microsoft.SharePoint.Client;
using MOHU.Integration.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly string _siteUrl;
        private readonly string _username;
        private readonly string _password;
        private ClientContext _clientContext;
        public DocumentService()
        {
            Initialize().Wait();
        }
        public async Task<(Stream fileStream, string fileName)> DownloadAttachmentByFileIdAsync(string fileId, string libraryName)
        {
            if (_clientContext == null)
            {
                throw new InvalidOperationException("SharePoint service has not been initialized.");
            }

            var library = _clientContext.Web.Lists.GetByTitle(libraryName);
            var file = library.GetItemById(fileId);

            _clientContext.Load(file, f => f.File.Name);
            await _clientContext.ExecuteQueryAsync();

            var fileName = file.File.Name;
            var fileStream = new MemoryStream();
            file.File.OpenBinaryStream().Value.CopyTo(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);

            return (fileStream, fileName);
        }



        public Task Initialize()
        {
            var securePassword = new SecureString();
            foreach (char c in _password)
            {
                securePassword.AppendChar(c);
            }

            _clientContext = new ClientContext(_siteUrl)
            {
                Credentials = new NetworkCredential(_username, _password)
            };
            return Task.CompletedTask;

        }
    }
}

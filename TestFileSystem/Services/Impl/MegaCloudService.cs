using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CG.Web.MegaApiClient;
using TestFileSystem.Models;

namespace TestFileSystem.Services.Impl
{
    public class MegaCloudService : ICloudService
    {
        private readonly MegaApiClient client;

        public MegaCloudService(string username, string password) {
            client = new MegaApiClient();
            client.Login(username, password);
        }

        ~MegaCloudService() {
            if (client.IsLoggedIn) {
                client.Logout();
            }
        }

        public Task<Stream> Download(ActionFileDTO file)
        {
            return client.DownloadAsync(file.Uri);
        }

        public async Task<ActionFileDTO> Upload(Stream dataStream, string fileName)
        {
            IEnumerable<INode> nodes = await client.GetNodesAsync();
            INode root = nodes.Single(x => x.Type == NodeType.Root);
            INode myFile = await client.UploadAsync(dataStream, fileName, root);
            Uri downloadLink = await client.GetDownloadLinkAsync(myFile);
            return new ActionFileDTO() {
                FileName = fileName,
                Uri = downloadLink
            };
        }
    }
}
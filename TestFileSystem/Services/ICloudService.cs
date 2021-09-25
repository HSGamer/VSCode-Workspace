using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestFileSystem.Models;

namespace TestFileSystem.Services
{
    public interface ICloudService
    {
        Task<ActionFileDTO> Upload(Stream dataStream, string fileName);

        Task<Stream> Download(ActionFileDTO file);
    }
}
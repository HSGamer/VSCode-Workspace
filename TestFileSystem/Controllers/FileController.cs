using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestFileSystem.Context;
using TestFileSystem.Models;
using TestFileSystem.Services;

namespace TestFileSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ICloudService cloudService;
        private readonly FileDbContext dbContext;

        public FileController(ICloudService cloudService, FileDbContext dbContext) {
            this.cloudService = cloudService;
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<FileDTO> Upload(IFormFile formFile)
        {
            string fileName = formFile.FileName;
            Stream stream = formFile.OpenReadStream();
            ActionFileDTO actionFileDTO = await cloudService.Upload(stream, fileName);
            FileEntry entry = actionFileDTO.ToEntry();
            dbContext.Files.Add(entry);
            await dbContext.SaveChangesAsync();
            return entry.ToDTO();
        }
    }
}
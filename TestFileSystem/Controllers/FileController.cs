using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public FileController(ICloudService cloudService, FileDbContext dbContext)
        {
            this.cloudService = cloudService;
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<FileDTO>> Upload(IFormFile formFile)
        {
            string fileName = formFile.FileName;
            Stream stream = formFile.OpenReadStream();
            ActionFileDTO actionFileDTO = await cloudService.Upload(stream, fileName);
            FileEntry entry = actionFileDTO.ToEntry();
            dbContext.Files.Add(entry);
            await dbContext.SaveChangesAsync();
            FileDTO fileDTO = entry.ToDTO();
            return CreatedAtAction(nameof(Get), new { id = fileDTO.Id }, fileDTO);
        }

        [HttpGet]
        public IEnumerable<FileDTO> GetAll()
        {
            return dbContext.Files.AsEnumerable().Select(file => file.ToDTO());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FileDTO>> Get(int id)
        {
            FileEntry entry = await dbContext.Files.FindAsync(id);
            if (entry is null)
            {
                return NotFound();
            }
            return Ok(entry.ToDTO());
        }

        [HttpGet("{id}/download")]
        public async Task<ActionResult> Download(int id) {
            FileEntry entry = await dbContext.Files.FindAsync(id);
            if (entry is null)
            {
                return NotFound();
            }
            ActionFileDTO fileDTO = entry.ToActionDTO();
            Stream stream = await cloudService.Download(fileDTO);
            using (var memoryStream = new MemoryStream()) {
                await stream.CopyToAsync(memoryStream);
                return new FileContentResult(memoryStream.ToArray(), "application/octet-stream") {
                    FileDownloadName = fileDTO.FileName
                };
            }
        }
    }
}
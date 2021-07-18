using IISTestApplication.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IISTestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public FileController(DataContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpPost("ToBase64String")]
        public IActionResult ToBase64(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            for (int i = 0; i < message.Length; i++)
            {
                var chCode = (byte)message[i];
                var isEqual = chCode == bytes[i];
            }

            var base64 = Convert.ToBase64String(bytes);

            return Ok(base64);
        }

        [HttpPost("FromBase64String")]
        public IActionResult FromBase64(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            var message = Encoding.UTF8.GetString(bytes);

            return Ok(message);
        }

        [HttpPost("UploadToFolder")]
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        public async Task<IActionResult> UploadAsync(IFormFile file, CancellationToken token)
        {
            if (file is not null)
            {
                var path = "/Files/" + file.FileName;

                using (var stream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(stream, token);
                }

                var fileMetadata = new FileMetadata { Name = file.FileName, Path = path };
                _context.FileMetadatas.Add(fileMetadata);
                await _context.SaveChangesAsync();

                return Ok();
            }
            
            return BadRequest();
        }

        [HttpGet("DownloadFromFolder/{fileName}")]
        public async Task<IActionResult> DownloadAsync(string fileName)
        {
            var file = await _context.FileMetadatas.FirstOrDefaultAsync(fm => fm.Name == fileName);
            if (file is null)
            {
                return BadRequest();
            }

            var stream = new FileStream(_appEnvironment.WebRootPath + file.Path, FileMode.Open, FileAccess.Read);

            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);

            return File(stream, contentType, fileName);
        }

        [HttpPost("UploadToDB")]
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        public async Task<IActionResult> UploadToDBAsync(IFormFile file, CancellationToken token)
        {
            if (file is not null)
            {
                using (var reader = new BinaryReader(file.OpenReadStream()))
                {
                    var fileDB = new Models.File { FileBody = reader.ReadBytes((int)file.Length) };
                    var fileMetadata = new FileMetadata { Name = file.FileName, File = fileDB };
                    _context.Files.Add(fileDB);
                    _context.FileMetadatas.Add(fileMetadata);
                }

                await _context.SaveChangesAsync(token);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("DownloadFromDB/{fileName}")]
        public async Task<IActionResult> DownloadFromDBAsync(string fileName)
        {
            var file = await _context.FileMetadatas.Include(fm => fm.File).FirstOrDefaultAsync(fm => fm.Name == fileName && fm.FileId != null);
            if (file is null)
            {
                return BadRequest();
            }

            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);

            return File(file.File.FileBody, contentType, fileName);
        }
    }
}

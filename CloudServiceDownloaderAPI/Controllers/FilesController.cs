using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudServiceDownloaderAPI.Contexts;
using CloudServiceDownloaderAPI.Models;
using CloudServiceDownloaderAPI.Models.DTO_s;

namespace CloudServiceDownloaderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly APIContext _context;

        public FilesController(APIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает список всех файлов
        /// </summary>
        // GET: api/Files
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileDTO>>> GetFiles()
        {
            var filesDTO = new List<FileDTO>();

            var files = await _context.Files.ToListAsync();

            foreach (var file in files)
            {
                var fileDTO = new FileDTO
                {
                    FileId = file.FileId,
                    FileName = file.FileName,
                    DownloadTime = file.DownloadTime,
                    ShareLinkId = file.ShareLinkId
                };

                filesDTO.Add(fileDTO);
            }

            return filesDTO;
        }

        /// <summary>
        /// Возвращает конкретный файл по его индентификатору
        /// </summary>
        /// <param name="id">Индентфиикатор файла</param>
        // GET: api/Files/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FileDTO>> GetFile(long id)
        {
            var file = await _context.Files.FindAsync(id);

            if (file == null)
            {
                return NotFound();
            }

            var fileDTO = new FileDTO
            {
                FileId = file.FileId,
                FileName = file.FileName,
                DownloadTime = file.DownloadTime,
                ShareLinkId = file.ShareLinkId
            };

            return fileDTO;
        }

        /// <summary>
        /// Удаляет файл по его индентификатору
        /// </summary>
        /// <param name="id">Индентфиикатор файла</param>
        // DELETE: api/Files/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(long id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            _context.Files.Remove(file);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

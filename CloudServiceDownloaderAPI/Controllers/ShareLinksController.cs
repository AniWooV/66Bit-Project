using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudServiceDownloaderAPI.Contexts;
using CloudServiceDownloaderAPI.Enums;
using CloudServiceDownloaderAPI.Models;
using CloudServiceDownloaderAPI.Models.DTO_s;
using CloudServiceDownloaderAPI.Services.Download;

namespace CloudServiceDownloaderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareLinksController : ControllerBase
    {
        private readonly APIContext _context;

        public ShareLinksController(APIContext context)
        {
            _context = context;
        }

        // GET: api/ShareLinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShareLinkDetailsDTO>>> GetShareLinks()
        {
            var shareLinks = await _context.ShareLinks.ToListAsync();

            var shareLinksToReturn = new List<ShareLinkDetailsDTO>();

            foreach (var shareLink in shareLinks)
            {
                var shareLinkDetailsDTO = new ShareLinkDetailsDTO
                {
                    ShareLinkId = shareLink.ShareLinkId,
                    Link = shareLink.Link,
                    CloudService = shareLink.CloudService,
                    IsDownloaded = shareLink.IsDownloaded
                };

                var filesDTO = new List<FileDTO>();

                var files = _context.Files
                    .Where(f => f.ShareLinkId == shareLink.ShareLinkId)
                    .ToList();

                foreach (var file in files)
                {
                    var fileDTO = new FileDTO
                    {
                        FileId = file.FileId,
                        FileName = file.FileName,
                        FileType = file.FileType,
                        DownloadTime = file.DownloadTime,
                        ShareLinkId = file.ShareLinkId
                    };

                    filesDTO.Add(fileDTO);
                }

                shareLinkDetailsDTO.Files = filesDTO;

                shareLinksToReturn.Add(shareLinkDetailsDTO);
            }

            return shareLinksToReturn;
        }

        // GET: api/ShareLinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShareLinkDetailsDTO>> GetShareLink(long id)
        {
            var shareLink = await _context.ShareLinks.FindAsync(id);

            if (shareLink == null)
            {
                return NotFound();
            }

            var shareLinkDetailsDTO = new ShareLinkDetailsDTO
            {
                ShareLinkId = shareLink.ShareLinkId,
                Link = shareLink.Link,
                CloudService = shareLink.CloudService,
                IsDownloaded = shareLink.IsDownloaded
            };

            var filesDTO = new List<FileDTO>();

            var files = _context.Files
                .Where(f => f.ShareLinkId == shareLink.ShareLinkId)
                .ToList();

            foreach (var file in files)
            {
                var fileDTO = new FileDTO
                {
                    FileId = file.FileId,
                    FileName = file.FileName,
                    FileType = file.FileType,
                    DownloadTime = file.DownloadTime,
                    ShareLinkId = file.ShareLinkId
                };

                filesDTO.Add(fileDTO);
            }

            shareLinkDetailsDTO.Files = filesDTO;

            return shareLinkDetailsDTO;
        }

        // POST: api/ShareLinks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShareLinkDetailsDTO>> PostShareLink(ShareLinkDTO shareLinkDTO)
        {
            var cloudService = DownloadHelper.GetCloudService(shareLinkDTO.Link);

            var shareLink = new ShareLink
            {
                Link = shareLinkDTO.Link,
                CloudService = cloudService,
                IsDownloaded = false
            };

            _context.ShareLinks.Add(shareLink);

            if (cloudService != CloudService.NoService)
            {
                var downloadHelper = new DownloadHelper();

                await downloadHelper.DownloadFile(shareLink, _context);
            }

            await _context.SaveChangesAsync();

            var shareLinkDetailsDTO = new ShareLinkDetailsDTO
            {
                ShareLinkId = shareLink.ShareLinkId,
                Link = shareLink.Link,
                CloudService = cloudService,
                IsDownloaded = false
            };

            return CreatedAtAction(nameof(GetShareLink), new { id = shareLink.ShareLinkId }, shareLinkDetailsDTO);
        }

        // DELETE: api/ShareLinks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShareLink(long id)
        {
            var shareLink = await _context.ShareLinks.FindAsync(id);
            if (shareLink == null)
            {
                return NotFound();
            }

            _context.ShareLinks.Remove(shareLink);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

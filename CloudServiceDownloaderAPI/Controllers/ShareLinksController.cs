using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudServiceDownloaderAPI.Contexts;
using CloudServiceDownloaderAPI.Models;
using CloudServiceDownloaderAPI.Models.DTO_s;
using CloudServiceDownloaderAPI.Services.Download;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        public async Task<ActionResult<IEnumerable<ShareLink>>> GetShareLinks()
        {
            return await _context.ShareLinks.ToListAsync();
        }

        // GET: api/ShareLinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShareLink>> GetShareLink(long id)
        {
            var shareLink = await _context.ShareLinks.FindAsync(id);

            if (shareLink == null)
            {
                return NotFound();
            }

            var files = _context.Files
                .Where(f => f.ShareLinkId == shareLink.ShareLinkId)
                .ToList();

            shareLink.Files = files;

            return shareLink;
        }

        // PUT: api/ShareLinks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShareLink(long id, ShareLink shareLink)
        {
            if (id != shareLink.ShareLinkId)
            {
                return BadRequest();
            }

            _context.Entry(shareLink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShareLinkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShareLinks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShareLink>> PostShareLink(ShareLinkDTO shareLinkDTO)
        {
            var downloadHelper = new DownloadHelper();

            var isCloudService = downloadHelper.IsCloudService(shareLinkDTO.Link);

            var shareLink = new ShareLink
            {
                Link = shareLinkDTO.Link,
                CloudService = downloadHelper.GetCloudService(shareLinkDTO.Link),
                IsDownloaded = false,
                IsUploaded = false,
            };

            _context.ShareLinks.Add(shareLink);

            if (isCloudService)
            {
                await downloadHelper.DownloadFile(shareLink, _context);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShareLink), new { id = shareLink.ShareLinkId }, shareLink);
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

        private bool ShareLinkExists(long id)
        {
            return _context.ShareLinks.Any(e => e.ShareLinkId == id);
        }
    }
}

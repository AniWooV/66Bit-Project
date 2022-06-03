using CloudServiceDownloaderAPI.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CloudServiceDownloaderAPI.Models
{
    //[Index(nameof(Link), IsUnique = true)]
    public class ShareLink
    {
        public long ShareLinkId { get; set; }
        public string Link { get; set; }
        public CloudService CloudService { get; set; }
        public bool IsDownloaded { get; set; }
        public bool IsUploaded { get; set; }

        public ICollection<File> Files { get; set; }

        public void UpdateModel(bool isDowloaded, bool isUploaded)
        {
            IsDownloaded = isDowloaded;
            IsUploaded = isUploaded;
        }
    }
}

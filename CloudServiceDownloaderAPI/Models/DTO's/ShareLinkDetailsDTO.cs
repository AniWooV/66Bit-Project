using System.Collections.Generic;
using CloudServiceDownloaderAPI.Enums;

namespace CloudServiceDownloaderAPI.Models.DTO_s
{
    public class ShareLinkDetailsDTO
    {
        public long ShareLinkId { get; set; }
        public string Link { get; set; }
        public CloudService CloudService { get; set; }
        public bool IsDownloaded { get; set; }

        public ICollection<FileDTO> Files { get; set; }
    }
}

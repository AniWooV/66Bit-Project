using CloudServiceDownloaderAPI.Enums;
using System;

namespace CloudServiceDownloaderAPI.Models.DTO_s
{
    public class FileDTO
    {
        public long FileId { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public DateTime DownloadTime { get; set; }
        public DateTime UploadTime { get; set; }

        public long ShareLinkId { get; set; }
    }
}

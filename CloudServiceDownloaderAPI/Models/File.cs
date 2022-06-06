using CloudServiceDownloaderAPI.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace CloudServiceDownloaderAPI.Models
{
    [Index(nameof(FilePath), IsUnique = true)]
    public class File
    {
        public long FileId { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public DateTime DownloadTime { get; set; }
        public string FilePath { get; set; }

        public long ShareLinkId { get; set; }
        public ShareLink ShareLink { get; set; }

        public File()
        {

        }

        public File(string fileName, string filePath, ShareLink shareLink)
        {
            FileName = fileName;
            FileType = FileType.Document;
            FilePath = filePath;
            DownloadTime = DateTime.Now;
            ShareLinkId = shareLink.ShareLinkId;
            ShareLink = shareLink;
        }
    }
}

using CloudServiceDownloaderAPI.Models;

namespace CloudServiceDownloaderAPI.Services.Download
{
    public interface IDownloader
    {
        public File DownloadFile(ShareLink shareLink, string folderPath);
        public bool IsLinkLegit(string link);
    }
}

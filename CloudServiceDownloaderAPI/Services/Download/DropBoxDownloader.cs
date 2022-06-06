using CloudServiceDownloaderAPI.Enums;
using CloudServiceDownloaderAPI.Models;
using Dropbox.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CloudServiceDownloaderAPI.Services.Download
{
    public class DropBoxDownloader : IDownloader
    {   
        private readonly DropboxClient _dbx;

        public DropBoxDownloader()
        {
            _dbx = new DropboxClient(
                "sl.BJAZ5uTiTte-oJeOEDPU1GcYJP4wKuBkZ5IQ-3pTQL4OSkJYG8jU8mpxOihBZWn-_69ceFt8bWObn3OBzf4h-l41Gu088B6FYCciVih5RvtO2cZGatwG5u4ctWc1IuX5Uww7D4X1l32s");
        }

        public File DownloadFile(ShareLink shareLink, string folderPath)
        {
            using (var wClient = new WebClient())
            {
                var fileName = GetFileName(shareLink.Link);

                var localFilePath = DownloadHelper.GetLocaFilelPath(fileName);

                var uri = new Uri(GetDownloadLink(shareLink.Link));

                wClient.DownloadFileAsync(uri, localFilePath);

                return new File(fileName, localFilePath, shareLink);
            }
        }

        public static bool IsLinkLegit(string link)
        {
            return true;
        }

        public string GetFileName(string link)
        {
            var gettingDataTask = Task.Run(async () =>
                await _dbx.Sharing.GetSharedLinkMetadataAsync(link));

            var result = gettingDataTask.Result;

            if (result.IsFile)
            {
                return result.Name;
            }

            return result.Name + ".zip";
        }

        public string GetDownloadLink(string link)
        {
            if (link.Contains("?dl=1"))
            {
                return link;
            }

            if (link.Contains("?dl=0"))
            {
                return link.Replace("?dl=0", "?dl=1");
            }

            if (link.Contains("?"))
            {
                return link.Replace("?", "?dl=1&");
            }

            return link + "?dl=1";
        }
    }
}

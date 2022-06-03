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
                "sl.BI3zx5EB-4O0CoY_xQxFNQn6sz1dziruqrtrZjjcuy5iWU1l3NpgplA2x9NmSbOee5pZrbZMAD9IUEgf6wJ0ZAePUam5oG4Dw3-e-XLTarUXvoxSU1DhyCNPZuremuCKXQ0uCKI7m7Bk");
        }

        public File DownloadFile(ShareLink shareLink, string folderPath)
        {
            var file = new File();

            using (var wClient = new WebClient())
            {
                if (IsLinkLegit(shareLink.Link))
                {
                    var fileName = GetFileName(shareLink.Link);

                    var localFilePath = DownloadHelper.GetLocaFilelPath(fileName);

                    var uri = new Uri(GetDownloadLink(shareLink.Link));

                    wClient.DownloadFileAsync(uri, localFilePath);

                    file = new File(fileName, localFilePath, shareLink);
                }

                return file;
            }
        }

        public bool IsLinkLegit(string link)
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

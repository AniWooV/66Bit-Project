using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CloudServiceDownloaderAPI.Models;

namespace CloudServiceDownloaderAPI.Services.Download
{
    public class YandexDownloader : IDownloader
    {
        //Поле, содержащее "базу" для получения ссылки на скачивание
        private readonly string _baseAddress;

        public YandexDownloader()
        {
            _baseAddress =
                "https://cloud-api.yandex.net/v1/disk/public/resources/download?public_key=";
        }

        public File DownloadFile(ShareLink shareLink, string folderPath)
        {
            using (var wClient = new WebClient())
            {
                var fileData = GetFileData(shareLink.Link);

                var fileName = GetFileName(fileData);

                var localFilePath = DownloadHelper.GetLocaFilelPath(fileName);

                var uri = new Uri(GetDownloadLink(fileData));

                wClient.DownloadFileAsync(uri, localFilePath);

                return new File(fileName, localFilePath, shareLink);
            }
        }

        public string GetDownloadLink(string fileData)
        {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                fileData);

            return dictionary["href"];
        }

        public string GetFileName(string fileData)
        {
            var s = fileData.Split('&')[1];
            var index = s.IndexOf('=');
            
            return s.Substring(index + 1, s.Length - index - 1);
        }

        public static bool IsLinkLegit(string link)
        {
            return true;
        }

        public string GetFileData(string link)
        {
            var client = new HttpClient();

            var uri = new Uri(_baseAddress + link);

            var gettingResponseTask = Task.Run(async () =>
                await client.GetAsync(uri));

            var readingResponseTask = Task.Run(async () =>
                await gettingResponseTask.Result.Content.ReadAsStringAsync());

            return readingResponseTask.Result;
        }
    }
}

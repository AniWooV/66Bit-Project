﻿using System;
using System.Threading.Tasks;
using CloudServiceDownloaderAPI.Contexts;
using CloudServiceDownloaderAPI.Enums;
using CloudServiceDownloaderAPI.Models;
using CloudServiceDownloaderAPI.Services.DataBase;

namespace CloudServiceDownloaderAPI.Services.Download
{
    public class DownloadHelper
    {
        private static readonly string _folderPath = Environment.CurrentDirectory + @"\Files\";

        public DownloadHelper()
        {
            if (!System.IO.Directory.Exists(_folderPath))
            {
                System.IO.Directory.CreateDirectory(_folderPath);
            }
        }

        public static CloudService GetCloudService(string link)
        {
            return CloudService.Yandex;
        }

        public static bool IsCloudService(string link)
        {
            return true;
        }

        public static string GetLocaFilelPath(string fileName)
        {
            var localFilePath = _folderPath + fileName;

            var c = 1;

            while (System.IO.File.Exists(localFilePath))
            {
                localFilePath = _folderPath + $"{c}{fileName}";
                c++;
            }

            return localFilePath;
        }

        public async Task DownloadFile(ShareLink shareLink, APIContext context)
        {
            var dBFiller = new DataBaseFiller(context);

            var file = new File();

            switch (GetCloudService(shareLink.Link))
            {
                case CloudService.DropBox:
                    var dropBoxDownloader = new DropBoxDownloader();

                    file = await Task.Run(() => 
                        dropBoxDownloader.DownloadFile(shareLink, _folderPath));
                    break;

                case CloudService.Yandex:
                    var yandexDownloader = new YandexDownloader();

                    file = await Task.Run(() =>
                        yandexDownloader.DownloadFile(shareLink, _folderPath));
                    break;

                case CloudService.Google:
                    break;

                case CloudService.Mail:
                    break;
            }

            await dBFiller.AddFile(file);
        }
    }
}
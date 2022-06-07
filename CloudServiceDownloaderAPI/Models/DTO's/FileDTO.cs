using CloudServiceDownloaderAPI.Enums;
using System;

namespace CloudServiceDownloaderAPI.Models.DTO_s
{
    /// <summary>
    /// Информация о файле, скаченном с облачного хранилища
    /// </summary>
    public class FileDTO
    {
        /// <summary>
        /// Индентификатор файла
        /// </summary>
        public long FileId { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Время скачивания файла
        /// </summary>
        public DateTime DownloadTime { get; set; }

        /// <summary>
        /// Индентификатор ссылки, по которой был скчан файл
        /// </summary>
        public long ShareLinkId { get; set; }
    }
}

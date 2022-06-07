using System.Collections.Generic;
using CloudServiceDownloaderAPI.Enums;

namespace CloudServiceDownloaderAPI.Models.DTO_s
{
    /// <summary>
    /// Информация о ссылке с облачного хранилища
    /// </summary>
    public class ShareLinkDetailsDTO
    {
        /// <summary>
        /// Индентификатор ссылки
        /// </summary>
        public long ShareLinkId { get; set; }

        /// <summary>
        /// Ссылка
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Облачное хранилище
        /// </summary>
        public CloudService CloudService { get; set; }

        /// <summary>
        /// Скачен ли файл
        /// </summary>
        public bool IsDownloaded { get; set; }

        public ICollection<FileDTO> Files { get; set; }
    }
}

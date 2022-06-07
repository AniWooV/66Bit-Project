using System.ComponentModel.DataAnnotations;

namespace CloudServiceDownloaderAPI.Models.DTO_s
{
    /// <summary>
    /// Информация о ссылке с облачного хранилища
    /// </summary>
    public class ShareLinkDTO
    {
        /// <summary>
        /// Ссылка
        /// </summary>
        [Required]
        public string Link { get; set; }
    }
}

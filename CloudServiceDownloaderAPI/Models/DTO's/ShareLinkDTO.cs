using System.ComponentModel.DataAnnotations;

namespace CloudServiceDownloaderAPI.Models.DTO_s
{
    public class ShareLinkDTO
    {
        [Required]
        public string Link { get; set; }
    }
}

using System.ComponentModel;

namespace CloudServiceDownloaderAPI.Enums
{
    /// <summary>
    /// Облачное хранилище
    /// </summary>
    public enum CloudService
    {
        [Description("NoService")]
        NoService = 0,

        [Description("Dropbox")]
        Dropbox = 1,

        [Description("Yandex")]
        Yandex = 2,

        [Description("Google")]
        Google = 3,

        [Description("Mail.ru")]
        Mail = 4,
    }
}

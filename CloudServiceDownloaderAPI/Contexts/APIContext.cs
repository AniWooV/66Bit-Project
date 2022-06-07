using CloudServiceDownloaderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudServiceDownloaderAPI.Contexts
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {

        }

        public DbSet<ShareLink> ShareLinks { get; set; }
        public DbSet<File> Files { get; set; }
    }
}

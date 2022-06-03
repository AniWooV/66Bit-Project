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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShareLink>()
                .HasMany(s => s.Files)
                .WithOne(f => f.ShareLink)
                .IsRequired();
        }

        public DbSet<ShareLink> ShareLinks { get; set; }
        public DbSet<File> Files { get; set; }
    }
}

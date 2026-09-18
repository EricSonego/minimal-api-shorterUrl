using Microsoft.EntityFrameworkCore;
using minimal_api_shorterUrl.Models;

namespace minimal_api_shorterUrl.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // table urls
        public DbSet<UrlMapping> UrlMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // pk db
            modelBuilder.Entity<UrlMapping>()
                .HasKey(u => u.IdUrl);

            // index db
            modelBuilder.Entity<UrlMapping>()
                .HasIndex(u => u.ShortCode)
                .IsUnique();
        }
    }
}
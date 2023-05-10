using Microsoft.EntityFrameworkCore;
using quest_web.Models;
using quest_web_dotnet.Models;
using System.Net;
using System.Reflection.Metadata;

namespace quest_web;

public class APIDbContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Rating> ratings { get; set; }
    public DbSet<Post> posts { get; set; }
    public DbSet<Tag> tags { get; set; }
    public DbSet<PostTag> posttags{ get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connection = "server=127.0.0.1;database=blog;user=application;password=password";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(connection, serverVersion);
        }
    }
    public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostsId, pt.TagsId });
        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.PostsId);
        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(t => t.TagsId);
    }
}

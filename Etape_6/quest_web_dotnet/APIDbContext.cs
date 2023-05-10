using Microsoft.EntityFrameworkCore;
using quest_web.Models;
using quest_web_dotnet.Models;
using System.Net;
using System.Reflection.Metadata;

namespace quest_web;

public class APIDbContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Post> posts { get; set; }
    public DbSet<Tag> tags { get; set; }
    public DbSet<Rating> ratings { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connection = "server=127.0.0.1;database=quest_web;user=application;password=password";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(connection, serverVersion);
        }
    }
    public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
    { }
}

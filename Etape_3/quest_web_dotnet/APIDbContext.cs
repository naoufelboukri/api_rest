using Microsoft.EntityFrameworkCore;
using quest_web.Models;

namespace quest_web;

public class APIDbContext : DbContext
{
    public DbSet<User> User { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connection = "server=127.0.0.1;database=quest_web;user=application;password=password";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

        optionsBuilder.UseMySql(connection, serverVersion);
    }
    public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
    { }

}

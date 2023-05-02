using Microsoft.EntityFrameworkCore;
using quest_web.Models;
using System.Reflection.Metadata;

namespace quest_web;

public class APIDbContext : DbContext
{
    public DbSet<User> user { get; set; }
    public DbSet<Address> address { get; set; }
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Addresses)
            .WithOne(e => e.User)
            .HasForeignKey("UserId")
            .IsRequired();
    }

}

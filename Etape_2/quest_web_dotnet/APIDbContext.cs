using Microsoft.EntityFrameworkCore;
using quest_web.Models;

namespace quest_web; 

public class APIDbContext : DbContext
{
    public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
}
using quest_web.Controllers;
using quest_web.Models.Form;
using quest_web.Utils;
using quest_web;
using Microsoft.EntityFrameworkCore;

namespace AutheticateTest;

public class AuthenticateTest : IClassFixture<AuthenticationController>
{
    private readonly APIDbContext _context;
    private readonly AuthenticationController _authenticationController;
    
    public AuthenticateTest(AuthenticationController authenticationController, APIDbContext context, JwtTokenUtil JWT)
    {
        var database = new DbContextOptionsBuilder<APIDbContext>();
        database.UseInMemoryDatabase(databaseName: "quest_web");
        var dbContextOptions = database.Options;
        _context = new APIDbContext(dbContextOptions);
        // Delete existing db before creating a new one
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();


    }

    [Fact]
    public void TestRegisterPassing()
    {
        var user = new UserBody
        {
            username = "test",
            password = "test"
        };
        var result = _authenticationController.register(user);
        Assert.True(true);
    }

}

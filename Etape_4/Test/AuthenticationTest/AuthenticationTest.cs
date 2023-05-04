using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using quest_web;
using quest_web.Controllers;
using quest_web.Models;
using quest_web.Models.Form;
using quest_web.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text.Json.Nodes;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class AuthenticationTest
    {
        private readonly ITestOutputHelper output;

        public AuthenticationTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Register()
        {
            var database = new DbContextOptionsBuilder<APIDbContext>()
                            .UseInMemoryDatabase(databaseName: "quest_web")
                            .Options;
            var _context = new APIDbContext(database);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var _jwt = new JwtTokenUtil();

            var _authenticationController = new AuthenticationController(_context, _jwt);
            var user = new UserBody
            {
                username = "test",
                password = "test"
            };
            var result = _authenticationController.register(user);
            
            //test
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
        [Fact]
        public void RegisterDuplicata()
        {
            var database = new DbContextOptionsBuilder<APIDbContext>()
                            .UseInMemoryDatabase(databaseName: "quest_web")
                            .Options;
            var _context = new APIDbContext(database);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var _jwt = new JwtTokenUtil();
            var _authenticationController = new AuthenticationController(_context, _jwt);

            var user = new UserBody
            {
                username = "test",
                password = "test"
            };
            //first
            _authenticationController.register(user);
            //second
            var second = _authenticationController.register(user);

            //test
            Assert.IsType<ConflictObjectResult>(second.Result);
        }

        [Fact]
        public void Authenticate()
        {
            var database = new DbContextOptionsBuilder<APIDbContext>()
                            .UseInMemoryDatabase(databaseName: "quest_web")
                            .Options;
            var _context = new APIDbContext(database);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var _jwt = new JwtTokenUtil();
            var _authenticationController = new AuthenticationController(_context, _jwt);

            var user = new UserBody
            {
                username = "test",
                password = "test"
            };

            _authenticationController.register(user);
            var result = _authenticationController.authenticate(user);

            //test
            Assert.IsType<OkObjectResult>(result.Result);

            var obj = JsonConvert.SerializeObject(result);
            Assert.Contains("token", obj);
        }

        [Fact]
        public void meTest()
        {
            var database = new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: "quest_web")
                .Options;
            var _context = new APIDbContext(database);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var _jwt = new JwtTokenUtil();
            var _authenticationController = new AuthenticationController(_context, _jwt);

            var user = new UserBody
            {
                username = "test",
                password = "test"
            };

            _authenticationController.register(user);
            var authenticate = _authenticationController.authenticate(user);

            var obj = JsonConvert.SerializeObject(authenticate);

            JObject jsonToken = JObject.Parse(obj);
            string authorization = "Bearer " + (string)jsonToken["Result"]["Value"]["token"];

            var result = _authenticationController.me(authorization);

            var response = Assert.IsType<OkObjectResult>(result);

            Assert.IsType<UserDetails>(response.Value);
        }
    }
}
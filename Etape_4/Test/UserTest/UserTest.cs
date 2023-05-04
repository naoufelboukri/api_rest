using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using quest_web;
using quest_web.Controllers;
using quest_web.Models.Form;
using quest_web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Test
{
    public class UserTest
    {
        private readonly ITestOutputHelper output;
        public UserTest(ITestOutputHelper output) {
            this.output = output;
        }

        [Fact]
        public void userWithoutToken()
        {
            var _jwt = new JwtTokenUtil();
            var database = new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: "quest_web")
                .Options;
            var _context = new APIDbContext(database);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var _userController = new UserController(_context, _jwt);
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

            var result = _userController.allUsers(authorization);
        }
    }

}

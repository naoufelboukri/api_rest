using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using quest_web;
using quest_web_dotnet.Controllers;
using quest_web_dotnet.Models.Forms;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using System.Security.Authentication;
using System.Text.Json.Nodes;

namespace Tests
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
                Username = "test",
                Password = "test"
            };

            _authenticationController.register(user);
            var authenticate = _authenticationController.authenticate(user);

            var obj = JsonConvert.SerializeObject(authenticate);

            JObject jsonToken = JObject.Parse(obj);

            string authorization = "Bearer " + (string)jsonToken["Result"]["Value"]["token"];


            var context = new DefaultHttpContext();
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            var feature = new ExceptionHandlerFeature()
            {
                Error = new AuthenticationException("Unauthorized")
            };
            context.Features.Set<IExceptionHandlerFeature>(feature);

            _userController.ControllerContext.HttpContext.Response.CompleteAsync();

            var pagination = new PaginationParameters()
            {
                PageNumber = 1,
                PageSize = 10,
            };

            var result = _userController.getAll(pagination);

            output.WriteLine(result.ToString());
        }

        [Fact]
        public void userWithToken()
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
                Username = "test",
                Password = "test"
            };

            _authenticationController.register(user);
            var authenticate = _authenticationController.authenticate(user);

            var obj = JsonConvert.SerializeObject(authenticate);

            JObject jsonToken = JObject.Parse(obj);

            var pagination = new PaginationParameters()
            {
                PageNumber = 1,
                PageSize = 10,
            };

            var result = _userController.getAll(pagination);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void deleteUserRoleFailed()
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
                Username = "test",
                Password = "test"
            };

            _authenticationController.register(user);
            var authenticate = _authenticationController.authenticate(user);

            var obj = JsonConvert.SerializeObject(authenticate);

            JObject jsonToken = JObject.Parse(obj);

            string authorization = "Bearer " + (string)jsonToken["Result"]["Value"]["token"];

            var result = _userController.delete(authorization, 2);

            var response = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public void deleteUserAdminSuccess()
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
                Username = "test",
                Password = "test"
            };

            _authenticationController.register(user);
            var authenticate = _authenticationController.authenticate(user);

            var obj = JsonConvert.SerializeObject(authenticate);

            JObject jsonToken = JObject.Parse(obj);

            string authorization = "Bearer " + (string)jsonToken["Result"]["Value"]["token"];

            JsonObject request = new JsonObject();
            request["role"] = "ROLE_ADMIN";

            var resultPut = _userController.Edit(authorization, request, 1);

            authenticate = _authenticationController.authenticate(user);


            obj = JsonConvert.SerializeObject(authenticate);

            jsonToken = JObject.Parse(obj);

            authorization = "Bearer " + (string)jsonToken["Result"]["Value"]["token"];

            var resultDelete = _userController.delete(authorization, 1);

            var response = Assert.IsType<OkObjectResult>(resultDelete);

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using quest_web;
using quest_web_dotnet.Controllers;
using quest_web_dotnet.Models.Forms;
using quest_web_dotnet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using System.Security.Authentication;
using System.Text.Json.Nodes;

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
            var result = _userController.getUsers();

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

            string authorization = "Bearer " + (string)jsonToken["Result"]["Value"]["token"];

            var result = _userController.getUsers();

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

            var result = _userController.deleteUser(authorization, "2");

            var response = Assert.IsType<ObjectResult>(result);

            Assert.Equal(403, response.StatusCode);
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

            var resultPut = _userController.editUser(authorization, request, 1);

            authenticate = _authenticationController.authenticate(user);


            obj = JsonConvert.SerializeObject(authenticate);

            jsonToken = JObject.Parse(obj);

            authorization = "Bearer " + (string)jsonToken["Result"]["Value"]["token"];

            var resultDelete = _userController.deleteUser(authorization, "1");

            var response = Assert.IsType<OkObjectResult>(resultDelete);

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using quest_web;
using quest_web.Models;
using quest_web_dotnet.Controllers;
using quest_web_dotnet.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using System.Text.Json.Nodes;

namespace Test
{
    public class TagTest
    {
        private readonly ITestOutputHelper output;
        public TagTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void getTagsTest()
        {
            var database = new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: "quest_web")
                .Options;
            var _context = new APIDbContext(database);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var _jwt = new JwtTokenUtil();

            var _tagController = new TagController(_context, _jwt);

            var result = _tagController.getTags();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void getTagTest()
        {
            var _jwt = new JwtTokenUtil();
            var database = new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: "quest_web")
                .Options;
            var _context = new APIDbContext(database);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var _authenticationController = new AuthenticationController(_context, _jwt);
            var _userController = new UserController(_context, _jwt);
            var _tagController = new TagController(_context, _jwt);

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
            _userController.editUser(authorization, request, 1);
            authenticate = _authenticationController.authenticate(user);

            obj = JsonConvert.SerializeObject(authenticate);
            jsonToken = JObject.Parse(obj);
            authorization = "Bearer " + (string)jsonToken["Result"]["Value"]["token"];

            var tag = new TagBody
            {
                Name = "Stratégie"
            };

            _tagController.createTask(tag, authorization);

            var result = _tagController.getTag(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void createTagTest()
        {
            var _jwt = new JwtTokenUtil();
            var database = new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: "quest_web")
                .Options;
            var _context = new APIDbContext(database);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var _authenticationController = new AuthenticationController(_context, _jwt);
            var _userController = new UserController(_context, _jwt);
            var _tagController = new TagController(_context, _jwt);

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
            _userController.editUser(authorization, request, 1);
            authenticate = _authenticationController.authenticate(user);

            obj = JsonConvert.SerializeObject(authenticate);
            jsonToken = JObject.Parse(obj);
            authorization = "Bearer " + (string)jsonToken["Result"]["Value"]["token"];

            var tag = new TagBody
            {
                Name = "Stratégie"
            };

            var result = _tagController.createTask(tag, authorization);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void editTagTest()
        {
            var _jwt = new JwtTokenUtil();
            var database = new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: "quest_web")
                .Options;
            var _context = new APIDbContext(database);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var _authenticationController = new AuthenticationController(_context, _jwt);
            var _userController = new UserController(_context, _jwt);
            var _tagController = new TagController(_context, _jwt);

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
            _userController.editUser(authorization, request, 1);
            authenticate = _authenticationController.authenticate(user);

            obj = JsonConvert.SerializeObject(authenticate);
            jsonToken = JObject.Parse(obj);
            authorization = "Bearer " + (string)jsonToken["Result"]["Value"]["token"];

            var tag = new TagBody
            {
                Name = "Stratégie"
            };

            _tagController.createTask(tag, authorization);

            request = new JsonObject();
            request["name"] = "Sport";

            var result = _tagController.editTag(authorization, request, 1);

            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public void deleteTagTest()
        {
            var _jwt = new JwtTokenUtil();
            var database = new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: "quest_web")
                .Options;
            var _context = new APIDbContext(database);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var _authenticationController = new AuthenticationController(_context, _jwt);
            var _userController = new UserController(_context, _jwt);
            var _tagController = new TagController(_context, _jwt);

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
            _userController.editUser(authorization, request, 1);
            authenticate = _authenticationController.authenticate(user);

            obj = JsonConvert.SerializeObject(authenticate);
            jsonToken = JObject.Parse(obj);
            authorization = "Bearer " + (string)jsonToken["Result"]["Value"]["token"];

            var tag = new TagBody
            {
                Name = "Stratégie"
            };

            _tagController.createTask(tag, authorization);
            var result = _tagController.deleteTag(authorization, 1);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}

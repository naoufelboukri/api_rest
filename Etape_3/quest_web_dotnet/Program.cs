using quest_web;
using Microsoft.EntityFrameworkCore;
using quest_web.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<JwtTokenUtil>();


builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8090);
});

var connection = "server=127.0.0.1;database=quest_web;user=application;password=password";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));


builder.Services.AddDbContext<APIDbContext>(options =>
    options.UseMySql(connection, serverVersion).LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
);

builder.Services.AddAuthentication("Bearer").AddJwtBearer(option =>
{
    option.TokenValidationParameters = JwtTokenUtil.TokenValidationParameters;
/*    option.Events = new JwtBearerEvents
    {

        OnChallenge = async (context) =>
        {
            context.HandleResponse();
            if (context.AuthenticateFailure != null)
            {
                context.Response.StatusCode = 401;

                await context.HttpContext.Response.WriteAsJsonAsync("Token non valide");
            }
        }
    };*/

});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<APIDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
// app.Use(async (context, next) =>
// {
//    await next();
//    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
//    {
//        await context.Response.WriteAsync(JsonSerializer.Serialize(new {message = "ça marche pas"}));
//        await context.Response.WriteAsync(new ErrorDetails(
//        {
//            StatusCode = context.Response.StatusCode,
//            Message = "Internal Server Error."
//        });
//        //context.Response.StatusCode = (int)HttpStatusCode.OK;

//    }

//});
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        await context.Response.WriteAsJsonAsync("Token vide ou invalide");
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

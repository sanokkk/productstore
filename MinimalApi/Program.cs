using System.Net;

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<User>(_ =>  new User("Alex"));

var app = builder.Build();

app.MapGet("mapget",  async (HttpResponse response, User user) =>
{
    response.StatusCode = 200;
    await response.WriteAsJsonAsync(user);
});

app.Run();

record User(string Name);
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddReverseProxy()
    .LoadFromConfig(configuration.GetSection("ReverseProxy"));

var app = builder.Build();
app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapReverseProxy();

app.Run();
using Envelope.Integration.DependencyInjection;
using TagManagement.Application;
using TagManagement.OuterService.BackgroudService;
using TagManagement.Persistence;
using TagManagement.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddHostedService<TagBackgroundService>();

services.AddApplication();
services.AddPersistence(configuration);
services.AddIntegrationMessageBus(configuration);

services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyOrigin();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TagContext>();
    dbContext.Database.EnsureCreated();
}

app.UseCors();
app.Run();

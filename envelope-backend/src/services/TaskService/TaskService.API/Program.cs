using Microsoft.EntityFrameworkCore;
using TaskService.Application;
using TaskService.Persistence;
using TaskService.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddRouting(options => options.LowercaseUrls = true);

services
    .AddApplication(configuration)
    .AddPersistence(configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var eventStoreContext = scope.ServiceProvider.GetRequiredService<TaskEventStoreContext>();
    eventStoreContext.Database.Migrate();
    var projectionContext = scope.ServiceProvider.GetRequiredService<TaskWriteOnlyContext>();
    projectionContext.Database.Migrate(); 
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

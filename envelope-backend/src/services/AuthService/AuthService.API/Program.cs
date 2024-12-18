using AuthService.Application;
using AuthService.Persistence;
using AuthService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
services.AddControllers();
services.AddSwaggerGen();

services.AddApplication(configuration);
services.AddPersistence(configuration);

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
    var dbContext = scope.ServiceProvider.GetRequiredService<UserContext>();
    dbContext.Database.Migrate();
}

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
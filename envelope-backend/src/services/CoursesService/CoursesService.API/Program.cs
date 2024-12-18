using CoursesService.Application;
using CoursesService.Persistence;
using Envelope.Common.Auth;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
services.AddControllers();
services.AddSwaggerGen();

services
    .AddApplication(configuration)
    .AddPersistence(configuration)
    .AddEnvelopeAuth(configuration);

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
    var dbContext = scope.ServiceProvider.GetRequiredService<CourseContext>();
    dbContext.Database.Migrate();
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
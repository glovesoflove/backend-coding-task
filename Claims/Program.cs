using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using Claims;
using Claims.Auditing;
using Claims.Controllers;
using Claims.Repository;
using Claims.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<AuditContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ClaimsContext>(
    options =>
    {
        var client = new MongoClient(builder.Configuration.GetConnectionString("MongoDb"));
        var database = client.GetDatabase(builder.Configuration["MongoDb:DatabaseName"]);
        options.UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);
    }
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClaimsService, ClaimsService>();
builder.Services.AddScoped<ICoversService, CoversService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AuditContext>();
    context.Database.Migrate();
}

app.Run();

public partial class Program { }

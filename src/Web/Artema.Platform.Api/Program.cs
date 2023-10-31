using Artema.Platform.Api.Configurations;
using Artema.Platform.Api.Extensions;
using Artema.Platform.Application;
using Artema.Platform.Infrastructure.Common;
using Artema.Platform.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
var isTesting = builder.Environment.IsEnvironment("Testing");
var databaseConfiguration = DatabaseConfiguration.BuildConfiguration(builder.Configuration);

// Add services to the container.

builder.Services.AddOptions();
builder.Services.AddApiEndpoints();
    
builder.Services.AddUseCases();
builder.Services.AddDataInfrastructure();
builder.Services.AddCommonInfrastructure();
builder.Services.AddDbContextInfrastructure(
    isTesting
    ? string.Format(databaseConfiguration.ConnectionString, Guid.NewGuid())
    : databaseConfiguration.ConnectionString
);

// Configure the HTTP request pipeline.

var app = builder.Build();
app.UseApiEndpoints();

app.UseHttpsRedirection();
app.Run();

public partial class Program {}

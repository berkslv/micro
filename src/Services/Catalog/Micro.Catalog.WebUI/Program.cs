using Micro.Catalog.Application;
using Micro.Catalog.Infrastructure;
using Micro.Catalog.Infrastructure.Persistence.Application;
using Micro.Catalog.WebUI;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.InitDatabase();

app.MapControllers();

app.UseHealthChecks("/api/health");

app.Run();
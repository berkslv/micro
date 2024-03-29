using System.Reflection;
using Micro.Basket.Services;
using Micro.Basket.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();


builder.Services.AddStackExchangeRedisCache(opt =>
{
     opt.Configuration = builder.Configuration.GetConnectionString("Redis");
     opt.InstanceName = "Micro.Basket";
});

builder.Services.AddTransient<IBasketService, BasketService>();
builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.MapControllers();

app.UseHealthChecks("/api/health");

app.Run();
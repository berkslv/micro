using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
    {
        o.MetadataAddress = "http://localhost:5050/identity/realms/micro/.well-known/openid-configuration";
        o.RequireHttpsMetadata = false;
        o.Authority = "http://localhost:5050/realms/micro";
        o.Audience = "account";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("user", policy => policy.RequireClaim("realm_roles","user"));
    options.AddPolicy("admin", policy => policy.RequireClaim("realm_roles","admin"));
});

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();
app.Run();
using Scalar.AspNetCore;
using Solimus.API.Common.Extensions;
using Solimus.Application;
using Solimus.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("./Environments/AppEnv.json", optional: true, reloadOnChange: true);

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Solimus API");
        options.WithTheme(ScalarTheme.BluePlanet);
    });
}

app.UseHttpsRedirection();
app.MapAllEndpoints();
app.Run();
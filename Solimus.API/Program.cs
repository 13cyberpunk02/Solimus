using Carter;
using Solimus.API.Extensions;
using Solimus.Application;
using Solimus.Application.Services;
using Solimus.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var dbConnection = builder.Configuration["ConnectionStrings:SqlServerConnection"];

builder.Services.AddAuthorization();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddInfrastructure(dbConnection!);
builder.Services.AddApplication();
builder.Services.AddCarter();
builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(opt =>
{
    opt.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins(builder.Configuration["Jwt:Audience"]!);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();

#region ContextSeed

using var scope = app.Services.CreateScope();
try
{
    var contextSeedService = scope.ServiceProvider.GetService<ContextSeedService>();
    await contextSeedService.InitializeContextAsync();
}
catch(Exception ex)
{
    var logger =  scope.ServiceProvider.GetService<ILogger<Program>>();
    logger.LogError(ex.Message, "Не получилось иницилизировать  и добавить стартуемые данные в базу данных.");
}

#endregion

app.Run();

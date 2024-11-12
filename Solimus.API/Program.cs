using Carter;
using Solimus.API.Extensions;
using Solimus.Application;
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

app.Run();

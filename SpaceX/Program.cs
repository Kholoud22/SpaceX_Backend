using SpaceX;
using SpaceX.Application;
using SpaceX.Middleware;
using FluentValidation.AspNetCore;
using Serilog;
using System.Reflection;
using SpaceX.API;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog(Logger.ConfigureLogger);

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddCors();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddFluentValidation(options =>
        options.RegisterValidatorsFromAssemblies(new List<Assembly>() { typeof(ApplicationLayer).Assembly }));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCustomExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(
        options => options.WithOrigins("*").AllowAnyMethod()
    );
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

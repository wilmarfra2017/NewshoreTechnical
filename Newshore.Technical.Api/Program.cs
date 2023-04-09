using MediatR;
using Microsoft.EntityFrameworkCore;
using Newshore.Technical.Domain.Aggregates.Interfaces;
using Newshore.Technical.Domain.Domain;
using Newshore.Technical.Domain.Interfaces;
using Newshore.Technical.Infrastructure;
using Newshore.Technical.Infrastructure.Finders;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Infrastructure.Repository;
using Serilog;
using Serilog.Events;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.Dev.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Day)
);

builder.Services.AddSingleton<IFlightRepository, FlightRepository>();
builder.Services.AddSingleton<IFlightFinder, FlightFinder>();
builder.Services.AddSingleton<IJourneyRepository, JourneyRepository>();
builder.Services.AddSingleton<IJourneyFinder, JourneyFinder>();
builder.Services.AddSingleton<IJourneyFlightRepository, JourneyFlightRepository>();
builder.Services.AddSingleton<ITransportRepository, TransportRepository>();
builder.Services.AddSingleton<ITransportFinder, TransportFinder>();
builder.Services.AddSingleton<IJourneyFlightRepository, JourneyFlightRepository>();
builder.Services.AddDbContext<SqlServerDbContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"), ServiceLifetime.Singleton);

builder.Services.AddSingleton<IJourneyDomain, JourneyDomain>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), AppDomain.CurrentDomain.Load("Newshore.Technical.Domain"));
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // Ajusta el origen según sea necesario
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

   
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowReactApp");
app.Run();

using ISPHackerEarth.Api.Middlewares;
using ISPHackerEarth.Application;
using ISPHackerEarth.Infrastructure;
using ISPHackerEarth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Get Connection String
var dbConectionString = builder.Configuration.GetConnectionString("DbConnection");

// Add services to the container.
builder.Services.AddInfrastructures();
builder.Services.AddApplications();

builder.Services.AddDbContext<ISPDbContext>(option => option.UseSqlite(dbConectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Add exception middleware
builder.Services.AddExceptionHandler<ISPExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(opt => { });

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();

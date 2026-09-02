using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MediFlow.Service;
using MediFlow.Repo;
using MediFlow;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var DbConnectionString = builder.Configuration.GetConnectionString("DbConnectionString");
builder.Services.AddDbContext<AppDbContext>(optionsAction => optionsAction.UseSqlServer(DbConnectionString));
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<PatientRepo>();
builder.Services.AddCors(options => 
    options.AddPolicy("MediFlowUIpolicy", policy => 
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyMethod().
        AllowAnyHeader()
    ));
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<DoctorRepo>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<AppointmentRepo>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors("MediFlowUIpolicy");

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.Run();

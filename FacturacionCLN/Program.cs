using FacturacionCLN.Data;
using FacturacionCLN.Repositories.Interfaces;
using FacturacionCLN.Repositories;
using FacturacionCLN.Services;
using Microsoft.EntityFrameworkCore;
using FacturacionCLN.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FacturacionDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de servicios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ITasaCambioRepository, TasaCambioRepository>();
builder.Services.AddScoped<ITasaCambioService, TasaCambioService>();
builder.Services.AddTransient<ProductoService>();
builder.Services.AddTransient<FacturaService>();
builder.Services.AddTransient<ReporteVentasService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

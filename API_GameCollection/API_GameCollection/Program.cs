
using API_GameCollection.Models;
using API_GameCollection.Profiles;
using API_GameCollection.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Incorporar DbContext
builder.Services.AddDbContext<GameCollectionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBGameCollection")));

// Cargar controladores
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Incorporar autoMapper, toma todos los profiles de la aplicación y los registra en el contenedor de servicios
builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

// Registrar clase de contraseñas como servicio para inyección de dependencias, scoped: una instancia para cada request
builder.Services.AddScoped<PasswordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();

    app.UseSwaggerUI(c => { 
        // Ruta donde se expone el documento OpenAPI
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Game Collection V1");

        // Mostrar swagger directamente en la raíz
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

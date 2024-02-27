// Importar los espacios de nombres necesarios para el proyecto
using CRM.API.Properties.Endpoints;
using CRM.API.Models.DAL;
using Microsoft.EntityFrameworkCore;

// Crea un nuevo constructor de la aplicacion web.
var builder = WebApplication.CreateBuilder(args);

// Agrega servicios para habilitar la generacion de documentacion de API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura y agrega un contexto de base de datos para Entity Framework Core.
builder.Services.AddDbContext<CRMContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn"))
);

// Agrega una instancia de la clase CustomerDAL como un servicio para la inyeccion de dependencia.
builder.Services.AddScoped<CustomerDAL>();

// Construye la alicacion web.
var app = builder.Build();

// Agrega los puntos finales relacioados con los clientes a la aplicacion.
app.AddCustomerEndpoints();

// Verifica si la aplicacion se esta ejecutando en un entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    // Habilita el uso de Swagger para la documentacion de la API.
    app.UseSwagger();
    app.UseSwaggerUI();

}

// Agrega middLeware para redirigir las solicitudes HTTP a HTTPS.
app.UseHttpsRedirection();

// Ejecuta la aplicacion.
app.Run();

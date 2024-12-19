using Impinj_Reader.Hubs;
using Impinj_Reader.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ReaderService>();
builder.Services.AddSingleton<ReaderSettings>();
builder.Services.AddSignalR();

// Configurar CORS para permitir solicitudes desde varios frontends
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3001", "http://localhost:3002") // Permitir ambos dominios
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Permitir env�o de cookies o credenciales
    });
});


var app = builder.Build();

// Usar la pol�tica de CORS configurada
app.UseCors("FrontendPolicy");

// Configuraci�n de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Configurar endpoints
app.MapControllers();
app.MapHub<MessageHub>("/message");

app.Run();

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
// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .SetIsOriginAllowed(origin => true)  // Permitir todas las solicitudes de origen
              .AllowCredentials();                 // Permitir las credenciales necesarias para SignalR
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
// Usar CORS con la pol�tica configurada
app.UseCors("AllowAll");


app.UseAuthorization();

// Configurar endpoints
app.MapControllers();
app.MapHub<MessageHub>("/message");

app.Run();

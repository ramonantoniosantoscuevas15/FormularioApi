using FormularioApi.Entidades;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;
//inicio de area de los servicios
builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });
    opciones.AddPolicy("libre", configuracion =>
    {
        configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

//fin de area de los servicios
var app = builder.Build();
//inicio del area de los middleware
app.MapGet("/", () => "Hello World!");
app.MapGet("/persona", () =>
{
    var personas = new List<Persona> 
    { 
        new Persona
        {
            Id = 1,
            Nombre = "Ramon",
        }
    };
    return personas;
});
//fin de area de los middleware
app.Run();

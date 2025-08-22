using Backend.DTOs;
using Backend.Modelos;
using Backend.Services;
using Backend.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<IPeopleService, People2Service>(); // Al cambiar PeopleService por People2Service, ahora todos los controladores usarán People2Service en lugar de PeopleService, y así podemos probar diferentes implementaciones de IPeopleServices sin cambiar el código de los controladores.
// Inyección de dependencias: registra PeopleService como implementación de IPeopleServices.
/* Es como decirle al contenedor de ASP.NET Core:
“Cuando alguien pida un IPeopleServices, créalo usando PeopleService y mantenlo como singleton (Solo se creará UNA ÚNICA instancia de PeopleService).”*/
// MEJOR FORMA USANDO KEY - De esta manera tengo todas y en cada controlador uso la que necesito:
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService");
builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("people2Service");

builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton"); 
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped"); 
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");

builder.Services.AddScoped<IPostsService, PostsService>(); // Mi duda es ¿Si saco AddScoped AddHttpClient lo reemplaza?

builder.Services.AddScoped<IBeerService, BeerService>();

// HttpClient servicio jsonplaceholder
// Acá habló algo de patron de diseño factory pero no lo veo
// PONER SI O SI DEBAJO DE LOS SERVICIOS sino se pisa
builder.Services.AddHttpClient<IPostsService, PostsService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPosts"]);
});
// 👉 Con esto:
/* 1. En el controlador vos pedís un `IPostsService`:
        public PostsController(IPostsService service) { ... }
   👉 .NET busca “¿quién provee un `IPostsService`?”

2. Como registraste:
   builder.Services.AddHttpClient<IPostsService, PostsService>(c =>
        {
            c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts");
        });
   👉 .NET sabe que la implementación es `PostsService`.

3. En el constructor de `PostsService` vos pedís un `HttpClient`:
        public PostsService(HttpClient httpClient) { ... }
   👉 .NET entiende: *“ah, para crear `PostsService` necesito un `HttpClient` también”*.

4. Gracias a que usaste `AddHttpClient`, el framework se encarga de:
   * Crear ese `HttpClient`.
   * Configurarlo con la `BaseAddress` que vos pusiste.
   * Pasarlo al constructor de `PostsService`.

5. Resultado:
   * El controlador recibe un `IPostsService`.
   * Ese `IPostsService` es un `PostsService`.
   * Ese `PostsService` ya viene con su `HttpClient` configurado y listo para usar. ✅
*/

// Entity Framework Core y SQL Server
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection")));
// 👉 Este bloque registra el DbContext (StoreContext) en el contenedor de servicios de la app (El contenedor de dependencias (DI Container)).
//    - AddDbContext<StoreContext>: le dice a ASP.NET Core que cree y gestione StoreContext.
//    - options.UseSqlServer(...): configura EF Core para usar SQL Server como base de datos.
//    - builder.Configuration.GetConnectionString("StoreConnection"): obtiene la cadena de conexión
//      desde appsettings.json bajo el nombre "StoreConnection".
//    En resumen: con esto la app sabe cómo conectarse a la BD y permite inyectar StoreContext en controladores/servicios.

// Validators
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<IPeopleService, People2Service>(); // Al cambiar PeopleService por People2Service, ahora todos los controladores usar�n People2Service en lugar de PeopleService, y as� podemos probar diferentes implementaciones de IPeopleServices sin cambiar el c�digo de los controladores.
// Inyecci�n de dependencias: registra PeopleService como implementaci�n de IPeopleServices.
/* Es como decirle al contenedor de ASP.NET Core:
�Cuando alguien pida un IPeopleServices, cr�alo usando PeopleService y mantenlo como singleton (Solo se crear� UNA �NICA instancia de PeopleService).�*/
// MEJOR FORMA USANDO KEY - De esta manera tengo todas y en cada controlador uso la que necesito:
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService");
builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("people2Service");

builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton"); 
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped"); 
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient"); 

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

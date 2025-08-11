using DAO; // <— importante

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("GymDb");
Conexion.Configurar(conn);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<Web.Services.SocioService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<Web.Components.App>()
   .AddInteractiveServerRenderMode();

app.Run();

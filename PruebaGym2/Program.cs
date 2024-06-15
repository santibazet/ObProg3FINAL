using Microsoft.EntityFrameworkCore;
using PruebaGym2.Datos;

var builder = WebApplication.CreateBuilder(args);

//Configurar conexion
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Aquí, obtenemos la cadena de conexión desde la configuración de la aplicación.

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar middleware de sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Cookie esencial para funcionamiento de la sesión
});

var app = builder.Build();

// Configurar pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession(); // Middleware de sesiones

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
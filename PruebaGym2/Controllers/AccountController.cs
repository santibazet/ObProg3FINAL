using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaGym2.Datos;
using PruebaGym2.Models;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        ViewBag.ShowFooter = false;
        ViewBag.ShowNavbar = false;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string nombre, string contraseña)
    {
        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(contraseña))
        {
            ViewBag.Error = "Por favor, introduzca un nombre de usuario y una contraseña.";
            return View();
        }

        var usuario = await _context.Usuarios.SingleOrDefaultAsync(u => u.Nombre == nombre && u.Contraseña == contraseña);
        if (usuario == null)
        {
            ViewBag.Error = "Nombre de usuario o contraseña incorrectos.";
            return View();
        }

        // Aquí, puedes redirigir al usuario a la página principal después de iniciar sesión correctamente
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        ViewBag.ShowFooter = false;
        ViewBag.ShowNavbar = false;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        if (usuario.Contraseña != usuario.ConfirmarContraseña)
        {
            ModelState.AddModelError("ConfirmarContraseña", "Las contraseñas no coinciden.");
            return View(usuario);
        }

        _context.Add(usuario);
        await _context.SaveChangesAsync();

        // Redirigir a la vista de login después del registro
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}

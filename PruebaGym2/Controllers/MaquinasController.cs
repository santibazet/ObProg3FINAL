using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaGym2.Datos;
using PruebaGym2.Models;

namespace PruebaGym2.Controllers
{

    public class MaquinasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaquinasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string tipoOrden, int? localId)
        {
            var maquinas = _context.Maquinas.Include(m => m.Local).Include(m => m.TipoMaquina).AsQueryable();

            if (localId.HasValue)
            {
                maquinas = maquinas.Where(m => m.IdLocal == localId.Value);
            }

            switch (tipoOrden)
            {
                case "ascendente":
                    maquinas = maquinas.OrderBy(m => m.FechaCompra);
                    break;
                case "descendente":
                    maquinas = maquinas.OrderByDescending(m => m.FechaCompra);
                    break;
            }

            // Lista para mantener las máquinas que necesitan ser actualizadas
            List<Maquina> maquinasParaActualizar = new List<Maquina>();

            // Recorremos todas las máquinas para calcular su vida útil restante y actualizar su disponibilidad si es necesario
            foreach (var maquina in maquinas)
            {
                DateTime fechaCompra = maquina.FechaCompra;
                int vidaUtil = maquina.VidaUtil;
                DateTime fechaFinVidaUtil = fechaCompra.AddYears(vidaUtil);

                int añosRestantes = (fechaFinVidaUtil - DateTime.Now).Days / 365;

                if (añosRestantes <= 0 && maquina.Disponible)
                {
                    maquina.Disponible = false;
                    maquinasParaActualizar.Add(maquina); // Añadir a la lista de máquinas para actualizar
                }
            }

            // Si hay máquinas para actualizar, guardamos los cambios en la base de datos
            if (maquinasParaActualizar.Count > 0)
            {
                _context.UpdateRange(maquinasParaActualizar);
                await _context.SaveChangesAsync();
            }

            var locales = await _context.Locales.ToListAsync();

            var model = new MaquinasPorOrdenModel
            {
                TipoOrden = tipoOrden,
                LocalId = localId,
                OrdenList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Orden Normal" },
                new SelectListItem { Value = "ascendente", Text = "Por fecha ascendente" },
                new SelectListItem { Value = "descendente", Text = "Por fecha descendente" }
            },
                LocalList = new SelectList(locales, "IdLocal", "Nombre"),
                Maquinas = await maquinas.ToListAsync()
            };

            return View(model);
        }

        // GET: Maquinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maquina = await _context.Maquinas
                .Include(m => m.Local)
                .Include(m => m.TipoMaquina)
                .FirstOrDefaultAsync(m => m.IdMaquina == id);
            if (maquina == null)
            {
                return NotFound();
            }

            // Calcular la vida útil restante
            DateTime fechaCompra = maquina.FechaCompra;
            int vidaUtil = maquina.VidaUtil;
            DateTime fechaFinVidaUtil = fechaCompra.AddYears(vidaUtil);

            int añosRestantes = (fechaFinVidaUtil - DateTime.Now).Days / 365;

            var model = new MaquinaDetailsViewModel
            {
                Maquina = maquina,
                VidaUtilRestante = añosRestantes > 0 ? añosRestantes : 0 // Para evitar valores negativos

            };

            if(model.VidaUtilRestante <= 0)
            {
                maquina.Disponible = false;
                _context.Update(maquina);
                await _context.SaveChangesAsync();
            }

            return View(model);
        }

        // GET: Maquinas/Create
        public IActionResult Create()
            {
                ViewData["IdLocal"] = new SelectList(_context.Locales, "IdLocal", "Nombre"); 
                ViewData["IdTipoMaquina"] = new SelectList(_context.TipoMaquinas, "IdTipoMaquina", "NombreTipo");
                return View();
            }

        // POST: Maquinas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMaquina,FechaCompra,PrecioCompra,VidaUtil,Disponible,IdLocal,IdTipoMaquina")] Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maquina);
                await _context.SaveChangesAsync();

                // Actualizar la cantidad de máquinas
                var local = await _context.Locales.Include(l => l.MaquinasDeLocal)
                                                  .FirstOrDefaultAsync(l => l.IdLocal == maquina.IdLocal);
                var maquinasPorTipo = local.MaquinasDeLocal
                             .Where(m => m.TipoMaquina != null) // Filtra las máquinas que tienen un TipoMaquina nulo
                             .GroupBy(m => m.TipoMaquina.NombreTipo)
                             .Select(g => new MaquinaPorTipoViewModel
                             {
                                 NombreTipo = g.Key,
                                 Cantidad = g.Sum(m => m.Cantidad)
                             })
                             .ToList();

                // Lógica adicional si es necesario

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLocal"] = new SelectList(_context.Locales, "IdLocal", "Ciudad", maquina.IdLocal);
            ViewData["IdTipoMaquina"] = new SelectList(_context.TipoMaquinas, "IdTipoMaquina", "NombreTipo", maquina.IdTipoMaquina);
            return View(maquina);
        }

        // GET: Maquinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var maquina = await _context.Maquinas.FindAsync(id);
                if (maquina == null)
                {
                    return NotFound();
                }

                // Obtener la lista de locales para el dropdown
                ViewData["IdLocal"] = new SelectList(_context.Locales, "IdLocal", "Nombre", maquina.IdLocal);

                // Obtener la lista de tipos de máquinas para el dropdown
                ViewData["IdTipoMaquina"] = new SelectList(_context.TipoMaquinas, "IdTipoMaquina", "NombreTipo", maquina.IdTipoMaquina);

                return View(maquina);
            }

        // POST: Maquinas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMaquina,FechaCompra,PrecioCompra,VidaUtil,Disponible,IdLocal,IdTipoMaquina,Cantidad")] Maquina maquina)
        {
            if (id != maquina.IdMaquina)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Obtener la máquina original desde la base de datos
                    var originalMaquina = await _context.Maquinas
                        .Include(m => m.Local)
                        .FirstOrDefaultAsync(m => m.IdMaquina == id);

                    if (originalMaquina == null)
                    {
                        return NotFound();
                    }

                    // Actualizar las propiedades de la máquina original con los valores de la máquina modificada
                    originalMaquina.FechaCompra = maquina.FechaCompra;
                    originalMaquina.PrecioCompra = maquina.PrecioCompra;
                    originalMaquina.VidaUtil = maquina.VidaUtil;
                    originalMaquina.Disponible = maquina.Disponible;
                    originalMaquina.IdLocal = maquina.IdLocal;
                    originalMaquina.IdTipoMaquina = maquina.IdTipoMaquina;

                    // Guardar los cambios en la base de datos
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaquinaExists(maquina.IdMaquina))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLocal"] = new SelectList(_context.Locales, "IdLocal", "Nombre", maquina.IdLocal);
            ViewData["IdTipoMaquina"] = new SelectList(_context.TipoMaquinas, "IdTipoMaquina", "NombreTipo", maquina.IdTipoMaquina);
            return View(maquina);
        }






        // GET: Maquinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var maquina = await _context.Maquinas
                    .Include(m => m.Local)
                    .Include(m => m.TipoMaquina)
                    .FirstOrDefaultAsync(m => m.IdMaquina == id);
                if (maquina == null)
                {
                    return NotFound();
                }

                return View(maquina);
            }

        // POST: Maquinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina != null)
            {
                var local = await _context.Locales.Include(l => l.MaquinasDeLocal)
                                                  .FirstOrDefaultAsync(l => l.IdLocal == maquina.IdLocal);
                var maquinaPorTipo = local.MaquinasDeLocal.FirstOrDefault(m => m.IdTipoMaquina == maquina.IdTipoMaquina);
                if (maquinaPorTipo != null)
                {
                    maquinaPorTipo.Cantidad--;
                    if (maquinaPorTipo.Cantidad == 0)
                    {
                        local.MaquinasDeLocal.Remove(maquinaPorTipo);
                    }
                }

                _context.Maquinas.Remove(maquina);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }



        private bool MaquinaExists(int id)
            {
                return _context.Maquinas.Any(e => e.IdMaquina == id);
            }
        }
    }



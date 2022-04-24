#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToolWorkshop.Data;
using ToolWorkshop.Data.Entities;

namespace ToolWorkshop.Controllers
{
    public class MovementsController : Controller
    {
        private readonly DataContext _context;

        public MovementsController(DataContext context)
        {
            _context = context;
        }

        // GET: Movements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movements.ToListAsync());
        }

        // GET: Movements/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movement = await _context.Movements
                .Include(m => m.Details)
                .ThenInclude(d => d.Catalog)
                .ThenInclude(c => c.Tool)
                .ThenInclude(t => t.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movement == null)
            {
                return NotFound();
            }

            return View(movement);
        }

        // GET: Movements/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movement movement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movement);
        }

        // GET: Movements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movement = await _context.Movements.FindAsync(id);
            if (movement == null)
            {
                return NotFound();
            }
            return View(movement);
        }

        // POST: Movements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movement movement)
        {
            if (id != movement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovementExists(movement.Id))
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
            return View(movement);
        }

        // GET: Movements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movement = await _context.Movements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movement == null)
            {
                return NotFound();
            }

            return View(movement);
        }

        // POST: Movements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movement = await _context.Movements.FindAsync(id);
            _context.Movements.Remove(movement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovementExists(int id)
        {
            return _context.Movements.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movement Movement = await _context.Movements.FindAsync(id);
            if (Movement == null)
            {
                return NotFound();
            }

            Movement_Detail model = new()
            {
                Movement = Movement
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(Movement_Detail model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Movement_Detail movement_Detail = new()
                    {
                        Movement = await _context.Movements.FindAsync(model.Id),
                        Catalog = model.Catalog,
                        Remarks = model.Remarks
                    };
                    _context.Add(movement_Detail);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.Id });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Departamento / Estado con el mismo nombre en este país.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }
    }
}

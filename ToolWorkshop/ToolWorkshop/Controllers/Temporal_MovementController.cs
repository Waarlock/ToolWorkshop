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
    public class Temporal_MovementController : Controller
    {
        private readonly DataContext _context;

        public Temporal_MovementController(DataContext context)
        {
            _context = context;
        }

        // GET: Temporal_Movement
        public async Task<IActionResult> Index()
        {
            return View(await _context.temporal_movements.ToListAsync());
        }

        // GET: Temporal_Movement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temporal_Movement = await _context.temporal_movements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (temporal_Movement == null)
            {
                return NotFound();
            }

            return View(temporal_Movement);
        }

        // GET: Temporal_Movement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Temporal_Movement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Start_DateTime,End_DateTime")] Temporal_Movement temporal_Movement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(temporal_Movement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(temporal_Movement);
        }

        // GET: Temporal_Movement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temporal_Movement = await _context.temporal_movements.FindAsync(id);
            if (temporal_Movement == null)
            {
                return NotFound();
            }
            return View(temporal_Movement);
        }

        // POST: Temporal_Movement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Start_DateTime,End_DateTime")] Temporal_Movement temporal_Movement)
        {
            if (id != temporal_Movement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(temporal_Movement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Temporal_MovementExists(temporal_Movement.Id))
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
            return View(temporal_Movement);
        }

        // GET: Temporal_Movement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temporal_Movement = await _context.temporal_movements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (temporal_Movement == null)
            {
                return NotFound();
            }

            return View(temporal_Movement);
        }

        // POST: Temporal_Movement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var temporal_Movement = await _context.temporal_movements.FindAsync(id);
            _context.temporal_movements.Remove(temporal_Movement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Temporal_MovementExists(int id)
        {
            return _context.temporal_movements.Any(e => e.Id == id);
        }
    }
}

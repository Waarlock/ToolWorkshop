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
    public class PlanogramsController : Controller
    {
        private readonly DataContext _context;

        public PlanogramsController(DataContext context)
        {
            _context = context;
        }

        // GET: Planograms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Planograms.ToListAsync());
        }

        // GET: Planograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planogram = await _context.Planograms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planogram == null)
            {
                return NotFound();
            }

            return View(planogram);
        }

        // GET: Planograms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Planograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Name")] Planogram planogram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planogram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planogram);
        }

        // GET: Planograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planogram = await _context.Planograms.FindAsync(id);
            if (planogram == null)
            {
                return NotFound();
            }
            return View(planogram);
        }

        // POST: Planograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Name")] Planogram planogram)
        {
            if (id != planogram.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planogram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanogramExists(planogram.Id))
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
            return View(planogram);
        }

        // GET: Planograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planogram = await _context.Planograms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planogram == null)
            {
                return NotFound();
            }

            return View(planogram);
        }

        // POST: Planograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planogram = await _context.Planograms.FindAsync(id);
            _context.Planograms.Remove(planogram);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanogramExists(int id)
        {
            return _context.Planograms.Any(e => e.Id == id);
        }
    }
}

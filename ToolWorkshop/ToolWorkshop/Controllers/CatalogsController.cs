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
    public class CatalogsController : Controller
    {
        private readonly DataContext _context;

        public CatalogsController(DataContext context)
        {
            _context = context;
        }

        // GET: Catalogs
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Catalogs.Include(c => c.Planogram).Include(c => c.Tool);
            return View(await dataContext.ToListAsync());
        }

        // GET: Catalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs
                .Include(c => c.Planogram)
                .Include(c => c.Tool)
                .FirstOrDefaultAsync(m => m.SKU == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // GET: Catalogs/Create
        public IActionResult Create()
        {
            ViewData["PlanogramId"] = new SelectList(_context.Planograms, "Id", "Name");
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "EAN");
            return View();
        }

        // POST: Catalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ToolId,PlanogramId,SKU,ToolImageId")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlanogramId"] = new SelectList(_context.Planograms, "Id", "Name", catalog.PlanogramId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "EAN", catalog.ToolId);
            return View(catalog);
        }

        // GET: Catalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs.FindAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }
            ViewData["PlanogramId"] = new SelectList(_context.Planograms, "Id", "Name", catalog.PlanogramId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "EAN", catalog.ToolId);
            return View(catalog);
        }

        // POST: Catalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ToolId,PlanogramId,SKU,ToolImageId")] Catalog catalog)
        {
            if (id != catalog.SKU)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogExists(catalog.SKU))
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
            ViewData["PlanogramId"] = new SelectList(_context.Planograms, "Id", "Name", catalog.PlanogramId);
            ViewData["ToolId"] = new SelectList(_context.Tools, "Id", "EAN", catalog.ToolId);
            return View(catalog);
        }

        // GET: Catalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs
                .Include(c => c.Planogram)
                .Include(c => c.Tool)
                .FirstOrDefaultAsync(m => m.SKU == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // POST: Catalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalog = await _context.Catalogs.FindAsync(id);
            _context.Catalogs.Remove(catalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogExists(int id)
        {
            return _context.Catalogs.Any(e => e.SKU == id);
        }
    }
}

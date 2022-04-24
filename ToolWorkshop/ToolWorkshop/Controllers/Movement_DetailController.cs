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
    public class Movement_DetailController : Controller
    {
        private readonly DataContext _context;

        public Movement_DetailController(DataContext context)
        {
            _context = context;
        }

        // GET: Movement_Detail
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movement_Details.ToListAsync());
        }

        // GET: Movement_Detail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movement_Detail = await _context.Movement_Details
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movement_Detail == null)
            {
                return NotFound();
            }

            return View(movement_Detail);
        }

        // GET: Movement_Detail/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movement_Detail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Remarks,Retun_Remarks")] Movement_Detail movement_Detail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movement_Detail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movement_Detail);
        }

        // GET: Movement_Detail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movement_Detail = await _context.Movement_Details.FindAsync(id);
            if (movement_Detail == null)
            {
                return NotFound();
            }
            return View(movement_Detail);
        }

        // POST: Movement_Detail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Remarks,Retun_Remarks")] Movement_Detail movement_Detail)
        {
            if (id != movement_Detail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movement_Detail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Movement_DetailExists(movement_Detail.Id))
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
            return View(movement_Detail);
        }

        // GET: Movement_Detail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movement_Detail = await _context.Movement_Details
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movement_Detail == null)
            {
                return NotFound();
            }

            return View(movement_Detail);
        }

        // POST: Movement_Detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movement_Detail = await _context.Movement_Details.FindAsync(id);
            _context.Movement_Details.Remove(movement_Detail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Movement_DetailExists(int id)
        {
            return _context.Movement_Details.Any(e => e.Id == id);
        }
    }
}

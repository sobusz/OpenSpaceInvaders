using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSpaceInvaders.Data;
using OpenSpaceInvaders.Models;

namespace OpenSpaceInvaders.Controllers
{
    public class DesksModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DesksModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DesksModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.DesksModel.ToListAsync());
        }

        // GET: DesksModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desksModel = await _context.DesksModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desksModel == null)
            {
                return NotFound();
            }

            return View(desksModel);
        }

        // GET: DesksModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DesksModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] DesksModel desksModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(desksModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(desksModel);
        }

        // GET: DesksModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desksModel = await _context.DesksModel.FindAsync(id);
            if (desksModel == null)
            {
                return NotFound();
            }
            return View(desksModel);
        }

        // POST: DesksModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] DesksModel desksModel)
        {
            if (id != desksModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desksModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesksModelExists(desksModel.Id))
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
            return View(desksModel);
        }

        // GET: DesksModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desksModel = await _context.DesksModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desksModel == null)
            {
                return NotFound();
            }

            return View(desksModel);
        }

        // POST: DesksModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var desksModel = await _context.DesksModel.FindAsync(id);
            _context.DesksModel.Remove(desksModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesksModelExists(int id)
        {
            return _context.DesksModel.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSpaceInvaders.Data;
using OpenSpaceInvaders.Models;
using Microsoft.AspNetCore.Authorization;


namespace OpenSpaceInvaders.Controllers
{
    public class BookingModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookingModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookingModel.Include(b => b.Desk);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookingModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingModel = await _context.BookingModel
                .Include(b => b.Desk)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingModel == null)
            {
                return NotFound();
            }

            return View(bookingModel);
        }

        
        // GET: BookingModels/Create
        public IActionResult Create()
        {
            ViewData["DeskId"] = new SelectList(_context.DesksModel, "Id", "Id");
            return View();
        }

        // POST: BookingModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,BookingDate,CustomerId,Name,Surname,PhoneNumber,Email,DeskId")] BookingModel bookingModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeskId"] = new SelectList(_context.DesksModel, "Id", "Id", bookingModel.DeskId);
            return View(bookingModel);
        }

        // GET: BookingModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingModel = await _context.BookingModel.FindAsync(id);
            if (bookingModel == null)
            {
                return NotFound();
            }
            ViewData["DeskId"] = new SelectList(_context.DesksModel, "Id", "Id", bookingModel.DeskId);
            return View(bookingModel);
        }

        // POST: BookingModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookingDate,CustomerId,Name,Surname,PhoneNumber,Email,DeskId")] BookingModel bookingModel)
        {
            if (id != bookingModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingModelExists(bookingModel.Id))
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
            ViewData["DeskId"] = new SelectList(_context.DesksModel, "Id", "Id", bookingModel.DeskId);
            return View(bookingModel);
        }

        // GET: BookingModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingModel = await _context.BookingModel
                .Include(b => b.Desk)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingModel == null)
            {
                return NotFound();
            }

            return View(bookingModel);
        }

        // POST: BookingModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingModel = await _context.BookingModel.FindAsync(id);
            _context.BookingModel.Remove(bookingModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingModelExists(int id)
        {
            return _context.BookingModel.Any(e => e.Id == id);
        }
    }
}

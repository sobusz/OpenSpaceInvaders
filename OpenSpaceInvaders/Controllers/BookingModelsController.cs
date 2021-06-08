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
using Microsoft.AspNetCore.Identity;

namespace OpenSpaceInvaders.Controllers
{
    public class BookingModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BookingModelsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BookingModels
        public async Task<IActionResult> Index()
        {
            var appUser = await _userManager.GetUserAsync(User);

            var applicationDbContext = _context.BookingModel.Include(b => b.Desk).Where(x => x.CustomerId == appUser.Id);
            
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
            var appUser = await _userManager.GetUserAsync(User);
            var userEmail = appUser.Email;
            var userId = appUser.Id;
            //bookingModel.DeskId = Id;

            bookingModel.Email = userEmail;
            bookingModel.CustomerId = userId;

            if (ModelState.IsValid)
            {
                var dupa = _context.BookingModel.Where(x => x.BookingDate == bookingModel.BookingDate).Where(x => x.DeskId == bookingModel.DeskId);
                if (dupa.Count() > 0)
                {
                    return RedirectToAction(nameof(Create));

                }
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSpaceInvaders.Data;

namespace OpenSpaceInvaders.Controllers
{
    public class DesksController : Controller
    {
        // GET: Desks
        private readonly ApplicationDbContext _context;

        public DesksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.DesksModel.ToListAsync());
        }

        // GET: Desks/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Desks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Desks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Desks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Desks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Desks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Desks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
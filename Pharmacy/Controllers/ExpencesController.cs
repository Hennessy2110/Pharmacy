using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy;

namespace Pharmacy.Controllers
{
    public class ExpencesController : Controller
    {
        private readonly PharmacyContext _context;

        public ExpencesController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Expences
        public async Task<IActionResult> Index()
        {
            var pharmacyContext = _context.Expence.Include(e => e.Medicament);
            return View(await pharmacyContext.ToListAsync());
        }

        // GET: Expences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expence = await _context.Expence
                .Include(e => e.Medicament)
                .SingleOrDefaultAsync(m => m.ExpenseId == id);
            if (expence == null)
            {
                return NotFound();
            }

            return View(expence);
        }

        // GET: Expences/Create
        public IActionResult Create()
        {
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Annotation");
            return View();
        }

        // POST: Expences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseId,MedicamentId,DateOfSale,Count,SellingPrice")] Expence expence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Annotation", expence.MedicamentId);
            return View(expence);
        }

        // GET: Expences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expence = await _context.Expence.SingleOrDefaultAsync(m => m.ExpenseId == id);
            if (expence == null)
            {
                return NotFound();
            }
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Annotation", expence.MedicamentId);
            return View(expence);
        }

        // POST: Expences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseId,MedicamentId,DateOfSale,Count,SellingPrice")] Expence expence)
        {
            if (id != expence.ExpenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenceExists(expence.ExpenseId))
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
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Annotation", expence.MedicamentId);
            return View(expence);
        }

        // GET: Expences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expence = await _context.Expence
                .Include(e => e.Medicament)
                .SingleOrDefaultAsync(m => m.ExpenseId == id);
            if (expence == null)
            {
                return NotFound();
            }

            return View(expence);
        }

        // POST: Expences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expence = await _context.Expence.SingleOrDefaultAsync(m => m.ExpenseId == id);
            _context.Expence.Remove(expence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenceExists(int id)
        {
            return _context.Expence.Any(e => e.ExpenseId == id);
        }
    }
}

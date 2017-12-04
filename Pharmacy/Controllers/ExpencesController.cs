using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Models;
using Pharmacy.ViewModels;

namespace Pharmacy.Controllers
{
    public class ExpencesController : Controller
    {
        private readonly PharmacyContext _context;

        public ExpencesController(PharmacyContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "user, admin")]
        // GET: Expences
        public IActionResult Index(int? medicamentId, string dateOfSale, int? counts, double? sellingPrice, int page = 1, ExpencesSortState sortOrder = ExpencesSortState.MedicamentIdAsc)
        {
            int pageSize = 10;
            IQueryable<Expence> source = _context.Expence;

            if (medicamentId != null && medicamentId != 0)
            {
                source = source.Where(x => x.MedicamentId == medicamentId);
            }
            if (dateOfSale != null)
            {
                source = source.Where(x => x.DateOfSale.Contains(dateOfSale));
            }
            if (counts != null && counts != 0)
            {
                source = source.Where(x => x.Count == counts);
            }
            if (sellingPrice != null)
            {
                source = source.Where(x => x.SellingPrice == sellingPrice);
            }

            switch (sortOrder)
            {
                case ExpencesSortState.MedicamentIdAsc:
                    source = source.OrderBy(x => x.MedicamentId);
                    break;
                case ExpencesSortState.DateOfSaleAsc:
                    source = source.OrderByDescending(x => x.DateOfSale);
                    break;
                case ExpencesSortState.CountAsc:
                    source = source.OrderBy(x => x.Count);
                    break;
                case ExpencesSortState.SellingPriceAsc:
                    source = source.OrderByDescending(x => x.SellingPrice);
                    break;
                default:
                    source = source.OrderBy(x => x.MedicamentId);
                    break;
            }



            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            ExpencesViewModel ivm = new ExpencesViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortExpencesViewModel(sortOrder),
                FilterViewModel = new FilterExpencesViewModel(medicamentId, dateOfSale, counts, sellingPrice),
                Expences = items
            };
            return View(ivm);
        }

        [Authorize(Roles = "user, admin")]
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

        [Authorize(Roles = "user, admin")]
        // GET: Expences/Create
        public IActionResult Create()
        {
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Annotation");
            return View();
        }

        [Authorize(Roles = "user, admin")]
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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

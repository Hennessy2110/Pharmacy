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
    public class ArrivalsController : Controller
    {
        private readonly PharmacyContext _context;

        public ArrivalsController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Arrivals
        public async Task<IActionResult> Index()
        {
            var pharmacyContext = _context.Arrival.Include(a => a.Deliver).Include(a => a.Medicament);
            return View(await pharmacyContext.ToListAsync());
        }

        // GET: Arrivals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arrival = await _context.Arrival
                .Include(a => a.Deliver)
                .Include(a => a.Medicament)
                .SingleOrDefaultAsync(m => m.ArrivalId == id);
            if (arrival == null)
            {
                return NotFound();
            }

            return View(arrival);
        }

        // GET: Arrivals/Create
        public IActionResult Create()
        {
            ViewData["DeliverId"] = new SelectList(_context.Delivers, "DeliverId", "Name");
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Annotation");
            return View();
        }

        // POST: Arrivals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArrivalId,MedicamentId,ReceiptDate,Count,DeliverId,PurchasePrice")] Arrival arrival)
        {
            if (ModelState.IsValid)
            {
                _context.Add(arrival);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeliverId"] = new SelectList(_context.Delivers, "DeliverId", "Name", arrival.DeliverId);
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Annotation", arrival.MedicamentId);
            return View(arrival);
        }

        // GET: Arrivals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arrival = await _context.Arrival.SingleOrDefaultAsync(m => m.ArrivalId == id);
            if (arrival == null)
            {
                return NotFound();
            }
            ViewData["DeliverId"] = new SelectList(_context.Delivers, "DeliverId", "Name", arrival.DeliverId);
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Annotation", arrival.MedicamentId);
            return View(arrival);
        }

        // POST: Arrivals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArrivalId,MedicamentId,ReceiptDate,Count,DeliverId,PurchasePrice")] Arrival arrival)
        {
            if (id != arrival.ArrivalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arrival);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArrivalExists(arrival.ArrivalId))
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
            ViewData["DeliverId"] = new SelectList(_context.Delivers, "DeliverId", "Name", arrival.DeliverId);
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Annotation", arrival.MedicamentId);
            return View(arrival);
        }

        // GET: Arrivals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arrival = await _context.Arrival
                .Include(a => a.Deliver)
                .Include(a => a.Medicament)
                .SingleOrDefaultAsync(m => m.ArrivalId == id);
            if (arrival == null)
            {
                return NotFound();
            }

            return View(arrival);
        }

        // POST: Arrivals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var arrival = await _context.Arrival.SingleOrDefaultAsync(m => m.ArrivalId == id);
            _context.Arrival.Remove(arrival);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArrivalExists(int id)
        {
            return _context.Arrival.Any(e => e.ArrivalId == id);
        }
    }
}

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
    public class MedicamentsController : Controller
    {
        private readonly PharmacyContext _context;

        public MedicamentsController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Medicaments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medicaments.ToListAsync());
        }

        // GET: Medicaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicaments = await _context.Medicaments
                .SingleOrDefaultAsync(m => m.MedicamentId == id);
            if (medicaments == null)
            {
                return NotFound();
            }

            return View(medicaments);
        }

        // GET: Medicaments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicamentId,Name,Annotation,Producer,Units,Storage")] Medicaments medicaments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicaments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicaments);
        }

        // GET: Medicaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicaments = await _context.Medicaments.SingleOrDefaultAsync(m => m.MedicamentId == id);
            if (medicaments == null)
            {
                return NotFound();
            }
            return View(medicaments);
        }

        // POST: Medicaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicamentId,Name,Annotation,Producer,Units,Storage")] Medicaments medicaments)
        {
            if (id != medicaments.MedicamentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicaments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicamentsExists(medicaments.MedicamentId))
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
            return View(medicaments);
        }

        // GET: Medicaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicaments = await _context.Medicaments
                .SingleOrDefaultAsync(m => m.MedicamentId == id);
            if (medicaments == null)
            {
                return NotFound();
            }

            return View(medicaments);
        }

        // POST: Medicaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicaments = await _context.Medicaments.SingleOrDefaultAsync(m => m.MedicamentId == id);
            _context.Medicaments.Remove(medicaments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicamentsExists(int id)
        {
            return _context.Medicaments.Any(e => e.MedicamentId == id);
        }
    }
}

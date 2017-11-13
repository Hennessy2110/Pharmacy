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
    public class DeliversController : Controller
    {
        private readonly PharmacyContext _context;

        public DeliversController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Delivers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Delivers.ToListAsync());
        }

        // GET: Delivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivers = await _context.Delivers
                .SingleOrDefaultAsync(m => m.DeliverId == id);
            if (delivers == null)
            {
                return NotFound();
            }

            return View(delivers);
        }

        // GET: Delivers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Delivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliverId,Surname,Name,Patronymic,ContactPhone")] Delivers delivers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delivers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delivers);
        }

        // GET: Delivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivers = await _context.Delivers.SingleOrDefaultAsync(m => m.DeliverId == id);
            if (delivers == null)
            {
                return NotFound();
            }
            return View(delivers);
        }

        // POST: Delivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliverId,Surname,Name,Patronymic,ContactPhone")] Delivers delivers)
        {
            if (id != delivers.DeliverId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delivers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliversExists(delivers.DeliverId))
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
            return View(delivers);
        }

        // GET: Delivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivers = await _context.Delivers
                .SingleOrDefaultAsync(m => m.DeliverId == id);
            if (delivers == null)
            {
                return NotFound();
            }

            return View(delivers);
        }

        // POST: Delivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delivers = await _context.Delivers.SingleOrDefaultAsync(m => m.DeliverId == id);
            _context.Delivers.Remove(delivers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliversExists(int id)
        {
            return _context.Delivers.Any(e => e.DeliverId == id);
        }
    }
}

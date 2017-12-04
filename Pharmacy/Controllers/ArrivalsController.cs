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
using System.Globalization;

namespace Pharmacy.Controllers
{
    public class ArrivalsController : Controller
    {
        private readonly PharmacyContext _context;

        public ArrivalsController(PharmacyContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "user, admin")]
        // GET: Arrivals
        public IActionResult Index(int? medicamentId, string receiptDate, string receiptDateFrom, string receiptDateTo, int? counts, int? deliverId, double? purchasePrice, string producer, int page = 1, ArrivalsSortState sortOrder = ArrivalsSortState.MedicamentIdAsc)
        {
            int pageSize = 10;
            IQueryable<Arrival> source = _context.Arrival;

            if (medicamentId != null && medicamentId != 0)
            {
                source = source.Where(x => x.MedicamentId == medicamentId);
            }
            if (receiptDate != null)
            {
                source = source.Where(x => x.ReceiptDate.Contains(receiptDate));
            }
            if (receiptDateFrom != null && receiptDateTo != null)
            {
                source = source.Where(x => DateTime.ParseExact(x.ReceiptDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(receiptDateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture) && DateTime.ParseExact(x.ReceiptDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) < DateTime.ParseExact(receiptDateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            }
            
            if (counts != null && counts != 0)
            {
                source = source.Where(x => x.Count == counts);
            }
            if (deliverId != null && deliverId != 0)
            {
                source = source.Where(x => x.DeliverId == deliverId);
            }
            if (purchasePrice != null && purchasePrice != 0)
            {
                source = source.Where(x => x.PurchasePrice == purchasePrice);
            }
            if (producer != null)
            {
                source = source.Where(x => x.Medicament.Producer.Contains(producer));
            }

            switch (sortOrder)
            {
                case ArrivalsSortState.MedicamentIdAsc:
                    source = source.OrderBy(x => x.MedicamentId);
                    break;
                case ArrivalsSortState.MedicamentIdDesc:
                    source = source.OrderByDescending(x => x.MedicamentId);
                    break;
                case ArrivalsSortState.ReceiptDateAsc:
                    source = source.OrderBy(x => x.ReceiptDate);
                    break;
                case ArrivalsSortState.ReceiptDateDesc:
                    source = source.OrderByDescending(x => x.ReceiptDate);
                    break;
                case ArrivalsSortState.CountAsc:
                    source = source.OrderBy(x => x.Count);
                    break;
                case ArrivalsSortState.CountDesc:
                    source = source.OrderByDescending(x => x.Count);
                    break;
                case ArrivalsSortState.DeliverIdAsc:
                    source = source.OrderBy(x => x.DeliverId);
                    break;
                case ArrivalsSortState.DeliverIdDesc:
                    source = source.OrderByDescending(x => x.DeliverId);
                    break;
                case ArrivalsSortState.PurchasePriceAsc:
                    source = source.OrderByDescending(x => x.PurchasePrice);
                    break;
                case ArrivalsSortState.PurchasePriceDesc:
                    source = source.OrderByDescending(x => x.PurchasePrice);
                    break;
                default:
                    source = source.OrderBy(x => x.MedicamentId);
                    break;
            }



            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            ArrivalsViewModel ivm = new ArrivalsViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortArrivalsViewModel(sortOrder),
                FilterViewModel = new FilterArrivalsViewModel(medicamentId, receiptDate, receiptDateFrom, receiptDateTo, counts, deliverId, purchasePrice, producer),
                Arrival = items
            };
            return View(ivm);
        }

        [Authorize(Roles = "user, admin")]
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

        [Authorize(Roles = "user, admin")]
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
        [Authorize(Roles = "user, admin")]
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

        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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

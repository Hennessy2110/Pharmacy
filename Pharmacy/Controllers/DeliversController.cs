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
    public class DeliversController : Controller
    {
        private readonly PharmacyContext _context;

        public DeliversController(PharmacyContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "user, admin")]
        // GET: Delivers
        public IActionResult Index(string surname, string name, string patronymic, int? contactPhone, int page=1, DeliversSortState sortOrder = DeliversSortState.SurnameAsc)
        {
            int pageSize = 10;
            IQueryable<Delivers> source = _context.Delivers;

            if (surname != null)
            {
                source = source.Where(x => x.Surname.Contains(surname));
            }
            if (name != null)
            {
                source = source.Where(x => x.Name.Contains(name));
            }
            if (patronymic != null)
            {
                source = source.Where(x => x.Patronymic.Contains(patronymic));
            }
            if (contactPhone != null && contactPhone != 0)
            {
                source = source.Where(x => x.ContactPhone == contactPhone);
            }

            switch (sortOrder)
            {
                case DeliversSortState.SurnameAsc:
                    source = source.OrderBy(x => x.Surname);
                    break;
                case DeliversSortState.SurnameDesc:
                    source = source.OrderByDescending(x => x.Surname);
                    break;
                case DeliversSortState.NameAsc:
                    source = source.OrderBy(x => x.Name);
                    break;
                case DeliversSortState.NameDesc:
                    source = source.OrderByDescending(x => x.Name);
                    break;
                case DeliversSortState.PatronymicAsc:
                    source = source.OrderBy(x => x.Patronymic);
                    break;
                case DeliversSortState.PatronymicDesc:
                    source = source.OrderByDescending(x => x.Patronymic);
                    break;
                case DeliversSortState.ContactPhoneAsc:
                    source = source.OrderBy(x => x.ContactPhone);
                    break;
                case DeliversSortState.ContactPhoneDesc:
                    source = source.OrderByDescending(x => x.ContactPhone);
                    break;
                default:
                    source = source.OrderBy(x => x.Surname);
                    break;
            }



            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            IndexViewModel ivm = new IndexViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortDeliversViewModel(sortOrder),
                FilterViewModel = new FilterDeliversViewModel(surname, name, patronymic, contactPhone),
                Deliver = items
            };
            return View(ivm);                        
        }

        [Authorize(Roles = "user, admin")]
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

        [Authorize(Roles = "user, admin")]
        // GET: Delivers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Delivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "user, admin")]
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

        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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

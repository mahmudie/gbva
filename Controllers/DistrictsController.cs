using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rmc.Models;
using Microsoft.AspNetCore.Authorization;

namespace rmc.Controllers
{
    [Authorize(Policy = "admin")]
    public class DistrictsController : Controller
    {
        private readonly rmsContext _context;

        public DistrictsController(rmsContext context)
        {
            _context = context;    
        }

        // GET: Districts
        public async Task<IActionResult> Index()
        {
            var rmsContext = _context.Districts.Include(d => d.ProvCodeNavigation);
            return View(await rmsContext.ToListAsync());
        }

        // GET: Districts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districts = await _context.Districts
                .Include(d => d.ProvCodeNavigation)
                .SingleOrDefaultAsync(m => m.DistCode == id);
            if (districts == null)
            {
                return NotFound();
            }

            return View(districts);
        }

        // GET: Districts/Create
        public IActionResult Create()
        {
            ViewData["ProvCode"] = new SelectList(_context.Provinces, "ProvCode", "ProvName");
            return View();
        }

        // POST: Districts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DistCode,DistName,DistNameDari,DistNamePashto,ProvCode")] Districts districts)
        {
            if (ModelState.IsValid)
            {
                districts.CreatedDate = DateTime.Now;
                _context.Add(districts);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ProvCode"] = new SelectList(_context.Provinces, "ProvCode", "ProvName", districts.ProvCode);
            return View(districts);
        }

        // GET: Districts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districts = await _context.Districts.SingleOrDefaultAsync(m => m.DistCode == id);
            if (districts == null)
            {
                return NotFound();
            }
            ViewData["ProvCode"] = new SelectList(_context.Provinces, "ProvCode", "ProvName", districts.ProvCode);
            return View(districts);
        }

        // POST: Districts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DistCode,DistName,DistNameDari,DistNamePashto,ProvCode")] Districts districts)
        {
            if (id != districts.DistCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Entry(districts).State = EntityState.Modified;
                    _context.Entry(districts).Property("CreatedDate").IsModified = false;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistrictsExists(districts.DistCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ProvCode"] = new SelectList(_context.Provinces, "ProvCode", "ProvName", districts.ProvCode);
            return View(districts);
        }

        // GET: Districts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var districts = await _context.Districts
                .Include(d => d.ProvCodeNavigation)
                .SingleOrDefaultAsync(m => m.DistCode == id);
            if (districts == null)
            {
                return NotFound();
            }

            return View(districts);
        }

        // POST: Districts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var districts = await _context.Districts.SingleOrDefaultAsync(m => m.DistCode == id);
            _context.Districts.Remove(districts);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DistrictsExists(string id)
        {
            return _context.Districts.Any(e => e.DistCode == id);
        }
    }
}

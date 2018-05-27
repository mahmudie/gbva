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
    public class ProvincesController : Controller
    {
        private readonly rmsContext _context;

        public ProvincesController(rmsContext context)
        {
            _context = context;    
        }

        // GET: Provinces
        public async Task<IActionResult> Index()
        {
            return View(await _context.Provinces.ToListAsync());
        }

        // GET: Provinces/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provinces = await _context.Provinces
                .SingleOrDefaultAsync(m => m.ProvCode == id);
            if (provinces == null)
            {
                return NotFound();
            }

            return View(provinces);
        }

        // GET: Provinces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Provinces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProvCode,Aghchocode,Aimscode,ProvName,ProveNameDari,ProveNamePashto")] Provinces province)
        {
            if (ModelState.IsValid)
            {
                province.CreatedDate = DateTime.Now;
                _context.Add(province);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(province);
        }

        // GET: Provinces/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provinces = await _context.Provinces.SingleOrDefaultAsync(m => m.ProvCode == id);
            if (provinces == null)
            {
                return NotFound();
            }
            return View(provinces);
        }

        // POST: Provinces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProvCode,Aghchocode,Aimscode,ProvName,ProveNameDari,ProveNamePashto")] Provinces provinces)
        {
            if (id != provinces.ProvCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(provinces).State=EntityState.Modified;
                    _context.Entry(provinces).Property("CreatedDate").IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvincesExists(provinces.ProvCode))
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
            return View(provinces);
        }

        // GET: Provinces/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provinces = await _context.Provinces
                .SingleOrDefaultAsync(m => m.ProvCode == id);
            if (provinces == null)
            {
                return NotFound();
            }

            return View(provinces);
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var provinces = await _context.Provinces.SingleOrDefaultAsync(m => m.ProvCode == id);
            _context.Provinces.Remove(provinces);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProvincesExists(string id)
        {
            return _context.Provinces.Any(e => e.ProvCode == id);
        }
    }
}

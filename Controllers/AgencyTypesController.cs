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
    public class AgencyTypesController : Controller
    {
        private readonly rmsContext _context;

        public AgencyTypesController(rmsContext context)
        {
            _context = context;    
        }

        // GET: AgencyTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgencyType.ToListAsync());
        }

        // GET: AgencyTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencyType = await _context.AgencyType
                .SingleOrDefaultAsync(m => m.AgencyTypeId == id);
            if (agencyType == null)
            {
                return NotFound();
            }

            return View(agencyType);
        }

        // GET: AgencyTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgencyTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgencyTypeId,AgencyType1")] AgencyType agencyType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agencyType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(agencyType);
        }

        // GET: AgencyTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencyType = await _context.AgencyType.SingleOrDefaultAsync(m => m.AgencyTypeId == id);
            if (agencyType == null)
            {
                return NotFound();
            }
            return View(agencyType);
        }

        // POST: AgencyTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgencyTypeId,AgencyType1")] AgencyType agencyType)
        {
            if (id != agencyType.AgencyTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agencyType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgencyTypeExists(agencyType.AgencyTypeId))
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
            return View(agencyType);
        }

        // GET: AgencyTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencyType = await _context.AgencyType
                .SingleOrDefaultAsync(m => m.AgencyTypeId == id);
            if (agencyType == null)
            {
                return NotFound();
            }

            return View(agencyType);
        }

        // POST: AgencyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agencyType = await _context.AgencyType.SingleOrDefaultAsync(m => m.AgencyTypeId == id);
            _context.AgencyType.Remove(agencyType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AgencyTypeExists(int id)
        {
            return _context.AgencyType.Any(e => e.AgencyTypeId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rmc.Models;

namespace rmc.Controllers
{
    public class AuthorizationSubsController : Controller
    {
        private readonly rmsContext _context;

        public AuthorizationSubsController(rmsContext context)
        {
            _context = context;    
        }

        // GET: AuthorizationSubs
        public async Task<IActionResult> Index()
        {
            var rmsContext = _context.AuthorizationSub.Include(a => a.AgencyType).Include(a => a.Authorization);
            return View(await rmsContext.ToListAsync());
        }

        // GET: AuthorizationSubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorizationSub = await _context.AuthorizationSub
                .Include(a => a.AgencyType)
                .Include(a => a.Authorization)
                .SingleOrDefaultAsync(m => m.AuthorizationSubId == id);
            if (authorizationSub == null)
            {
                return NotFound();
            }

            return View(authorizationSub);
        }

        // GET: AuthorizationSubs/Create
        public IActionResult Create()
        {
            ViewData["AgencyTypeId"] = new SelectList(_context.AgencyType, "AgencyTypeId", "AgencyTypeId");
            ViewData["AuthorizationId"] = new SelectList(_context.Authorization, "AuthorizationId", "AuthorizationId");
            return View();
        }

        // POST: AuthorizationSubs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorizationSubId,AuthorizationId,AgencyTypeId,AgencyName,Comment")] AuthorizationSub authorizationSub)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorizationSub);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AgencyTypeId"] = new SelectList(_context.AgencyType, "AgencyTypeId", "AgencyTypeId", authorizationSub.AgencyTypeId);
            ViewData["AuthorizationId"] = new SelectList(_context.Authorization, "AuthorizationId", "AuthorizationId", authorizationSub.AuthorizationId);
            return View(authorizationSub);
        }

        // GET: AuthorizationSubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorizationSub = await _context.AuthorizationSub.SingleOrDefaultAsync(m => m.AuthorizationSubId == id);
            if (authorizationSub == null)
            {
                return NotFound();
            }
            ViewData["AgencyTypeId"] = new SelectList(_context.AgencyType, "AgencyTypeId", "AgencyTypeId", authorizationSub.AgencyTypeId);
            ViewData["AuthorizationId"] = new SelectList(_context.Authorization, "AuthorizationId", "AuthorizationId", authorizationSub.AuthorizationId);
            return View(authorizationSub);
        }

        // POST: AuthorizationSubs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorizationSubId,AuthorizationId,AgencyTypeId,AgencyName,Comment")] AuthorizationSub authorizationSub)
        {
            if (id != authorizationSub.AuthorizationSubId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorizationSub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorizationSubExists(authorizationSub.AuthorizationSubId))
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
            ViewData["AgencyTypeId"] = new SelectList(_context.AgencyType, "AgencyTypeId", "AgencyTypeId", authorizationSub.AgencyTypeId);
            ViewData["AuthorizationId"] = new SelectList(_context.Authorization, "AuthorizationId", "AuthorizationId", authorizationSub.AuthorizationId);
            return View(authorizationSub);
        }

        // GET: AuthorizationSubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorizationSub = await _context.AuthorizationSub
                .Include(a => a.AgencyType)
                .Include(a => a.Authorization)
                .SingleOrDefaultAsync(m => m.AuthorizationSubId == id);
            if (authorizationSub == null)
            {
                return NotFound();
            }

            return View(authorizationSub);
        }

        // POST: AuthorizationSubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorizationSub = await _context.AuthorizationSub.SingleOrDefaultAsync(m => m.AuthorizationSubId == id);
            _context.AuthorizationSub.Remove(authorizationSub);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AuthorizationSubExists(int id)
        {
            return _context.AuthorizationSub.Any(e => e.AuthorizationSubId == id);
        }
    }
}

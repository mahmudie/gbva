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
    public class AuthorizationsController : Controller
    {
        private readonly rmsContext _context;

        public AuthorizationsController(rmsContext context)
        {
            _context = context;    
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorization = await _context.Authorization.Include(m=>m.GbvCase)
                .SingleOrDefaultAsync(m => m.GbvCase.GbvCaseId == id && m.GbvCase.UserName.Equals(User.Identity.Name));
            if (authorization == null)
            {
                return NotFound();
            }
            ViewData["GbvCaseId"] = new SelectList(_context.GbvCase, "GbvCaseId", "GbvCaseId", authorization.GbvCaseId);
            return View(authorization);
        }

        // POST: Authorizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorizationId,GbvCaseId,Islessthan18,IsSigned,UserName,LastUpdate,InsertDate")] Authorization authorization)
        {
            if (id != authorization.GbvCaseId)
            {
                return NotFound();
            }
            var item = await _context.Authorization.Include(m => m.GbvCase).AsNoTracking()
                .SingleOrDefaultAsync(m => m.GbvCase.GbvCaseId == id && m.GbvCase.UserName.Equals(User.Identity.Name));
            if (item == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    item.GbvCase.LastUpdate = DateTime.Now;
                    _context.Update(authorization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorizationExists(authorization.AuthorizationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("edit", authorization.GbvCaseId);
            }
            return View(authorization);
        }

        // GET: Authorizations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorization = await _context.Authorization
                .Include(a => a.GbvCase)
                .SingleOrDefaultAsync(m => m.AuthorizationId == id);
            if (authorization == null)
            {
                return NotFound();
            }

            return View(authorization);
        }

        // POST: Authorizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorization = await _context.Authorization.SingleOrDefaultAsync(m => m.AuthorizationId == id);
            _context.Authorization.Remove(authorization);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AuthorizationExists(int id)
        {
            return _context.Authorization.Any(e => e.AuthorizationId == id);
        }
    }
}

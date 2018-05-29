using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rmc.Models;
using rmc.helper;

namespace rmc.Controllers
{
    public class AuthorizationsController : Controller
    {
        private readonly rmsContext _context;
        private readonly ICipherService _crypto;

        public AuthorizationsController(rmsContext context, ICipherService crypto)
        {
            _context = context;
            _crypto = crypto;
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorization = await _context.Authorization.Include(m=>m.GbvCase).AsNoTracking()
                .SingleOrDefaultAsync(m => m.GbvCase.GbvCaseId == id && m.GbvCase.UserName.Equals(User.Identity.Name));
            if (authorization == null)
            {
                return NotFound();
            }
            var sub = _context.AuthorizationSub.SingleOrDefault(m => m.AuthorizationId == authorization.AuthorizationId);
            if (sub != null)
            {
                authorization.AgencyTypeId = sub.AgencyTypeId;
                authorization.AgencyName = sub.AgencyName;
                authorization.Comment = sub.Comment;
                ViewData["agency"] = new SelectList(_context.AgencyType.ToList(), "AgencyTypeId", "AgencyType1", sub.AgencyTypeId.GetValueOrDefault());
            }
            else
            {
                ViewData["agency"] = new SelectList(_context.AgencyType.ToList(), "AgencyTypeId", "AgencyType1");

            }
            authorization.GbvCase.PatientName = _crypto.Decrypt(authorization.GbvCase.PatientName);
            authorization.GbvCase.PatientFatherName = _crypto.Decrypt(authorization.GbvCase.PatientFatherName);
            ViewBag.details = authorization.GbvCase;

            return View(authorization);
        }

        // POST: Authorizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AgencyTypeId,AgencyName,Comment,AuthorizationId,GbvCaseId,Islessthan18,IsSigned,UserName,LastUpdate,InsertDate")] Authorization authorization)
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
                    var sub = _context.AuthorizationSub.SingleOrDefault(m => m.AuthorizationId == item.AuthorizationId);
                    if(sub == null)
                    {
                        var newSub = new AuthorizationSub();
                        newSub.AgencyTypeId = authorization.AgencyTypeId;
                        newSub.AgencyName = authorization.AgencyName;
                        newSub.Comment = authorization.Comment;
                        newSub.AuthorizationId = authorization.AuthorizationId;
                        _context.AuthorizationSub.Add(newSub);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        sub.AgencyTypeId = authorization.AgencyTypeId;
                        sub.AgencyName = authorization.AgencyName;
                        sub.Comment = authorization.Comment;
                    }
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

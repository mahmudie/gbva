using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rmc.Models;
using Microsoft.AspNetCore.Authorization;
using rmc.helper;

namespace rmc.Controllers
{
    [Authorize(Roles = "dataentry,administrator")]
    public class ConsentsController : Controller
    {
        private readonly rmsContext _context;
        private readonly ICipherService _crypto;


        public ConsentsController(rmsContext context,ICipherService crypto)
        {
            _context = context;
            _crypto = crypto;
        }

    

        // GET: Consents/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consent = await _context.Consent.Include(m=>m.GbvCase).AsNoTracking().SingleOrDefaultAsync(m => m.GbvCaseId == id && m.GbvCase.UserName.Equals(User.Identity.Name));
            if (consent == null)
            {
                return NotFound();
            }
            consent.GbvCase.PatientName = _crypto.Decrypt(consent.GbvCase.PatientName);
            consent.GbvCase.PatientFatherName = _crypto.Decrypt(consent.GbvCase.PatientFatherName);
            ViewBag.details = consent.GbvCase;
            return View(consent);
        }

        // POST: Consents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("ConsentId,GbvCaseId,PhysicalExams,PelvicExams,SpeculumExams,OtherExams,BloodExams,ProvisionOfInfo")] Consent consent)
        {
            if (id != consent.GbvCaseId)
            {
                return NotFound();
            }
            var item = await _context.Consent.Include(m => m.GbvCase).AsNoTracking().SingleOrDefaultAsync(m => m.GbvCaseId == id && m.GbvCase.UserName.Equals(User.Identity.Name));
            if (item == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    item.GbvCase.LastUpdate = DateTime.Now;
                    _context.Update(consent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsentExists(consent.ConsentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit",new { Guid =consent.GbvCaseId });
            }
            ViewData["GbvCaseId"] = new SelectList(_context.GbvCase, "GbvCaseId", "GbvCaseId", consent.GbvCaseId);
            return View(consent);
        }


        private bool ConsentExists(int id)
        {
            return _context.Consent.Any(e => e.ConsentId == id);
        }
    }
}

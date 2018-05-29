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
    public class RegistrationController : Controller
    {
        private readonly rmsContext _context;
        private readonly ICipherService _crypto;
        public RegistrationController(rmsContext context, ICipherService crypto)
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

            var registration = await _context.Registration.Include(m=>m.GbvCase).SingleOrDefaultAsync(m => m.GbvCaseId == id && m.GbvCase.UserName.Equals(User.Identity.Name));
            if (registration == null)
            {
                return NotFound();
            }
            registration.GbvCase.PatientName = _crypto.Decrypt(registration.GbvCase.PatientName);
            registration.GbvCase.PatientFatherName = _crypto.Decrypt(registration.GbvCase.PatientFatherName);
            ViewBag.details = registration.GbvCase;
            return View(registration);
        }

        // POST: Registration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("RegistrationId,GbvCaseId,RptYear,RptMonth,IsIdp,TypeOfViolence,GbvHistory,RefIn,RefOut,ServiceMedical,ServicePsycho,ServiceRefLegal,ServiceRefSafeHouse,Remarks")] Registration registration)
        {
            if (id != registration.GbvCaseId)
            {
                return NotFound();
            }

            var item = await _context.Registration.Include(m => m.GbvCase).AsNoTracking().SingleOrDefaultAsync(m => m.GbvCaseId == id && m.GbvCase.UserName.Equals(User.Identity.Name));
            if (item == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    item.GbvCase.LastUpdate = DateTime.Now;
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.RegistrationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("edit", new { Guid = registration.GbvCaseId });
            
            }
            return View(registration);
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registration.Any(e => e.RegistrationId == id);
        }
    }
}

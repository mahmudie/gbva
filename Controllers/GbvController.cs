using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rmc.Models;
using Microsoft.AspNetCore.Authorization;
using rmc.Models.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace rmc.Controllers
{
    [Authorize(Roles = "dataentry,administrator")]
    public class GbvController : Controller
    {
        private readonly rmsContext _context;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private const string Key = "Salted__(etuD3F[´Ú£Ù,PüGÉŸ{Óé";

        public GbvController(rmsContext context,IDataProtectionProvider dataProtectionProvider)
        {
            _context = context;
            _dataProtectionProvider = dataProtectionProvider;
        }

        // GET: Gbv
        public async Task<IActionResult> Index()
        {
            var rmsContext = _context.GbvCase.Include(g => g.Hospital).Where(m => m.UserName.Equals(User.Identity.Name));
            return View(await rmsContext.ToListAsync());
        }
        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> list()
        {
            var rmsContext = _context.GbvCase.Include(g => g.Hospital);
            return View(await rmsContext.ToListAsync());
        }

        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> show(Guid? id)
        {
            var report = new GbvReport();
            report.IntakeInfo=await _context.IntakeInfo.Where(m => m.GbvCase.GbvCaseId.Equals(id)).SingleOrDefaultAsync();
            report.Authorization = await _context.Authorization.Where(m => m.GbvCase.GbvCaseId.Equals(id)).SingleOrDefaultAsync();
            report.Consent = await _context.Consent.Where(m => m.GbvCase.GbvCaseId.Equals(id)).SingleOrDefaultAsync();
            report.Registration= await _context.Registration.Where(m => m.GbvCase.GbvCaseId.Equals(id)).SingleOrDefaultAsync();
            return View(report);
        }
        // GET: Gbv/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gbvCase = await _context.GbvCase
                .Include(g => g.Hospital)
                .SingleOrDefaultAsync(m => m.GbvCaseId == id && m.UserName.Equals(User.Identity.Name));
            if (gbvCase == null)
            {
                return NotFound();
            }

            return View(gbvCase);
        }

        // GET: Gbv/Create
        public IActionResult Create()
        {
            var facilities = _context.FacilityInfo.Where(m => m.User.Equals(User.Identity.Name)).ToList();
            ViewData["HospitalId"] = new SelectList(facilities, "FacilityId", "FacilityName");
            return View();
        }

        // POST: Gbv/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItrCode,HospitalId,RegNo,PatientName,PatientFatherName,Age,Sex,Address,MaritalStatus,RefDate,RefTime")] GbvCase gbvCase)
        {
            if (ModelState.IsValid)
            {

                    var name = Encrypt(gbvCase.PatientName);
                    gbvCase.PatientName = name;
                    gbvCase.GbvCaseId = Guid.NewGuid();
                    gbvCase.UserName = User.Identity.Name;
                    gbvCase.IncCode = String.Format("{0}-{1}", gbvCase.HospitalId, gbvCase.RegNo);
                    gbvCase.InsertDate = DateTime.Now;
                    gbvCase.LastUpdate = DateTime.Now;
                    _context.Add(gbvCase);
                    var consent = new Consent();
                    consent.GbvCaseId = gbvCase.GbvCaseId;
                    _context.Add(consent);
                    var reg = new Registration();
                    reg.GbvCaseId = gbvCase.GbvCaseId;
                    _context.Add(reg);
                    var intake = new IntakeInfo();
                    intake.GbvCaseId = gbvCase.GbvCaseId;
                    _context.Add(intake);
                    var auth = new Authorization();
                    auth.GbvCaseId = gbvCase.GbvCaseId;
                    _context.Add(auth);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("edit", "consents", new { id= gbvCase.GbvCaseId });
                
            }
            var facilities = _context.FacilityInfo.Where(m => m.User.Equals(User.Identity.Name)).ToList();
            ViewData["HospitalId"] = new SelectList(facilities, "FacilityId", "FacilityName", gbvCase.HospitalId);
            return View(gbvCase);
        }

        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(input);
        }

        public string Decrypt(string cipherText)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }

        // GET: Gbv/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var gbvCase = await _context.GbvCase.SingleOrDefaultAsync(m => m.GbvCaseId == id && m.UserName.Equals(User.Identity.Name));
            if (gbvCase == null)
            {
                return NotFound();
            }
            gbvCase.PatientName = Decrypt(gbvCase.PatientName);
            var facilities = _context.FacilityInfo.Where(m => m.User.Equals(User.Identity.Name)).ToList();
            ViewData["HospitalId"] = new SelectList(facilities, "FacilityId", "FacilityName", gbvCase.HospitalId);
            return View(gbvCase);
        }

        // POST: Gbv/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("HospitalId,GbvCaseId,RptCode,RegNo,ItrCode,PatientName,PatientFatherName,Age,Sex,Address,MaritalStatus,RefDate,RefTime,LastUpdate")] GbvCase gbvCase)
        {
            if (id != gbvCase.GbvCaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var row = await _context.GbvCase
                          .Include(g => g.Hospital).AsNoTracking()
                          .SingleOrDefaultAsync(m => m.GbvCaseId == id && m.UserName.Equals(User.Identity.Name));
                if (row == null)
                {
                    return NotFound();
                }
                try
                {
                    gbvCase.LastUpdate = DateTime.Now;
                    gbvCase.IncCode = String.Format("{0}-{1}", gbvCase.HospitalId, gbvCase.RegNo);
                    _context.Entry(gbvCase).State=EntityState.Modified;
                    _context.Entry(gbvCase).Property("UserName").IsModified = false;
                    _context.Entry(gbvCase).Property("InsertDate").IsModified = false;


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GbvCaseExists(gbvCase.GbvCaseId))
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
            var facilities = _context.FacilityInfo.Where(m => m.User.Equals(User.Identity.Name)).ToList();
            ViewData["HospitalId"] = new SelectList(facilities, "FacilityId", "FacilityName", gbvCase.HospitalId);
            return View(gbvCase);
        }

        // GET: Gbv/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gbvCase = await _context.GbvCase
                .Include(g => g.Hospital)
                .SingleOrDefaultAsync(m => m.GbvCaseId == id && m.UserName.Equals(User.Identity.Name));
            if (gbvCase == null)
            {
                return NotFound();
            }

            gbvCase.PatientName = Decrypt(gbvCase.PatientName);
            return View(gbvCase);
        }

        // POST: Gbv/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gbvCase = await _context.GbvCase.SingleOrDefaultAsync(m => m.GbvCaseId == id && m.UserName.Equals(User.Identity.Name));
            _context.GbvCase.Remove(gbvCase);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Consents(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gbvCase = await _context.Consent
                .SingleOrDefaultAsync(m => m.GbvCaseId == id);
            if (gbvCase == null)
            {
                return NotFound();
            }

            return View(gbvCase);
        }

        private bool GbvCaseExists(Guid id)
        {
            return _context.GbvCase.Any(e => e.GbvCaseId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rmc.Models;
using Microsoft.AspNetCore.Authorization;
using rmc.Data;

namespace rmc.Controllers
{
    [Authorize(Policy = "admin")]
    public class FacilitiesController : Controller
    {
        private readonly rmsContext _context;
        private readonly ApplicationDbContext _db;


        public FacilitiesController(rmsContext context, ApplicationDbContext db)
        {
            _context = context;
            _db = db;
        }

        // GET: Facilities
        public async Task<IActionResult> Index()
        {
            var rmsContext = _context.FacilityInfo.Include(f => f.DistCodeNavigation);
            return View(await rmsContext.ToListAsync());
        }

        // GET: Facilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityInfo = await _context.FacilityInfo
                .Include(f => f.DistCodeNavigation)
                .SingleOrDefaultAsync(m => m.FacilityId == id);
            if (facilityInfo == null)
            {
                return NotFound();
            }

            return View(facilityInfo);
        }

        // GET: Facilities/Create
        public IActionResult Create()
        {
            var users = _db.Users.Where(m => m.Active.Equals(true)).Select(m => new ApplicationUser()
            {
                FirstName = m.FirstName + " " + m.LastName + "-" + m.UserName,
                UserName = m.UserName,
            }).ToList();
            ViewData["User"] = new SelectList(users, "UserName", "FirstName");
            ViewData["DistCode"] = new SelectList(_context.Districts, "DistCode", "DistName");
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("User,FacilityId,ActiveStatus,DateEstablished,DistCode,FacilityName,FacilityNameDari,FacilityNamePashto,FacilityType,Gpslattitude,Gpslongtitude,Implementer,Lat,Location,LocationDari,LocationPashto,Lon,SubImplementer,ViliCode")] FacilityInfo facilityInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facilityInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var users = _db.Users.Where(m=>m.Active.Equals(true)).Select(m => new ApplicationUser()
            {
                FirstName = m.FirstName + " "+  m.LastName +"-"+ m.UserName,
                UserName = m.UserName,
            }).ToList();
            ViewData["User"] = new SelectList(users, "UserName", "FirstName", facilityInfo.User);

            ViewData["DistCode"] = new SelectList(_context.Districts, "DistCode", "DistName", facilityInfo.DistCode);
            return View(facilityInfo);
        }

        // GET: Facilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityInfo = await _context.FacilityInfo.SingleOrDefaultAsync(m => m.FacilityId == id);
            if (facilityInfo == null)
            {
                return NotFound();
            }

            var users = _db.Users.Where(m => m.Active.Equals(true)).Select(m => new ApplicationUser()
            {
                FirstName = m.FirstName + " " + m.LastName + "-" + m.UserName,
                UserName = m.UserName,
            }).ToList();
            ViewData["User"] = new SelectList(users, "UserName", "FirstName", facilityInfo.User);

            ViewData["DistCode"] = new SelectList(_context.Districts, "DistCode", "DistName", facilityInfo.DistCode);
            return View(facilityInfo);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacilityId,User,ActiveStatus,DateEstablished,DistCode,FacilityName,FacilityNameDari,FacilityNamePashto,FacilityType,Gpslattitude,Gpslongtitude,Implementer,Lat,Location,LocationDari,LocationPashto,Lon,SubImplementer,ViliCode")] FacilityInfo facilityInfo)
        {
            if (id != facilityInfo.FacilityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facilityInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityInfoExists(facilityInfo.FacilityId))
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

            var users = _db.Users.Where(m => m.Active.Equals(true)).Select(m => new ApplicationUser()
            {
                FirstName = m.FirstName + " " + m.LastName + "-" + m.UserName,
                UserName=m.UserName,
            }).ToList();
            ViewData["User"] = new SelectList(users, "UserName", "FirstName", facilityInfo.User);

            ViewData["DistCode"] = new SelectList(_context.Districts, "DistCode", "DistName", facilityInfo.DistCode);
            return View(facilityInfo);
        }

        // GET: Facilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityInfo = await _context.FacilityInfo
                .Include(f => f.DistCodeNavigation)
                .SingleOrDefaultAsync(m => m.FacilityId == id);
            if (facilityInfo == null)
            {
                return NotFound();
            }

            return View(facilityInfo);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facilityInfo = await _context.FacilityInfo.SingleOrDefaultAsync(m => m.FacilityId == id);
            _context.FacilityInfo.Remove(facilityInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FacilityInfoExists(int id)
        {
            return _context.FacilityInfo.Any(e => e.FacilityId == id);
        }
    }
}

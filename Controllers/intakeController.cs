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
    [Authorize(Roles = "dataentry,administrator")]
    public class intakeController : Controller
    {
        private readonly rmsContext _context;

        public intakeController(rmsContext context)
        {
            _context = context;    
        }

        // GET: intake/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intakeInfo = await _context.IntakeInfo.Include(m=>m.GbvCase).SingleOrDefaultAsync(m => m.GbvCaseId == id && m.GbvCase.UserName.Equals(User.Identity.Name));
            if (intakeInfo == null)
            {
                return NotFound();
            }
            ViewData["GbvCaseId"] = new SelectList(_context.GbvCase, "GbvCaseId", "GbvCaseId", intakeInfo.GbvCaseId);
            return View(intakeInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("formtype,IntakeInfoId,GbvCaseId,ReferredBy,RefOther,ExamDate,ExamTime,IncDate,IncTime,IncLocation,IncLocationOther,IncType,IncReportToOther,Relationship,NoOfPerpetrators,PerpetratorOccupation,OccupationOther,AgeGroup,StiStatus,HcvHbs,EvidenceOfPregnancy,PregnancyWeeks,Results,ResultsOther,PsychologicalStatus,ExaminedForAids,SstPrevenMed,MedForInjuries,VacForTreat,ReferToPsychosocial,ReferToHigherMedical,YesBetterFacilities,YesFpservices,YesVaccination,YesConsulation,YesVct,YesPregnancyTest,YesOther,NoServiceProvided,NoServiceAccepted,NoServiceAvailable,RefFpcenter,Mowadowa,Aihrc,SafeHouse,BackHome,Other,OtherSpecify,RefNotAvailable,RefNotAccepted,IsSurvivorWilling,EvidenceCollected,MedicalCertificate,Appointment,MedExamProcedure,ConsentTaken,SpreadInfo")] IntakeInfo info)
        {
            if (id != info.GbvCaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var item = _context.IntakeInfo.Where(m => m.GbvCaseId.Equals(info.GbvCaseId)).Include(m=>m.GbvCase).AsNoTracking().SingleOrDefault();
                if (!item.GbvCase.UserName.Equals(User.Identity.Name))
                {
                    return NotFound();
                }
                if (info.formtype == 1)
                {
                    item.ReferredBy = info.ReferredBy;
                    item.RefOther = info.RefOther;
                    item.ExamDate = info.ExamDate;
                    item.ExamTime = info.ExamTime;
                }
                if (info.formtype == 2)
                {
                    item.IncLocation = info.IncLocation;
                    item.IncType = info.IncType;
                    item.IncLocationOther = info.IncLocationOther;
                    item.IncReportToOther = info.IncReportToOther;
                    item.IncDate = info.IncDate;
                    item.IncTime = info.IncTime;
                }
                if (info.formtype == 3)
                {
                    item.Relationship = info.Relationship;
                    item.NoOfPerpetrators = info.NoOfPerpetrators;
                    item.PerpetratorOccupation = info.PerpetratorOccupation;
                    item.OccupationOther = info.OccupationOther;
                }
                if (info.formtype == 4)
                {
                    item.AgeGroup = info.AgeGroup;
                    item.StiStatus = info.StiStatus;
                    item.HcvHbs = info.HcvHbs;
                    item.EvidenceOfPregnancy = info.EvidenceOfPregnancy;
                    item.PregnancyWeeks = info.PregnancyWeeks;
                    item.Results = info.Results;
                    item.ResultsOther = info.ResultsOther;
                    item.PsychologicalStatus = info.PsychologicalStatus;
                    item.ExaminedForAids = info.ExaminedForAids;
                }
                if (info.formtype == 5)
                {
                    item.SstPrevenMed = info.SstPrevenMed;
                    item.MedForInjuries = info.MedForInjuries;
                    item.VacForTreat = info.VacForTreat;
                    
                }
                if (info.formtype == 6)
                {
                    item.ReferToPsychosocial = info.ReferToPsychosocial;
                    item.ReferToHigherMedical = info.ReferToHigherMedical;
                    item.YesBetterFacilities = info.YesBetterFacilities;
                    item.YesFpservices = info.YesFpservices;
                    item.YesVaccination = info.YesVaccination;
                    item.YesConsulation = info.YesConsulation;
                    item.YesVct = info.YesVct;
                    item.YesPregnancyTest = info.YesPregnancyTest;
                    item.YesOther = info.YesOther;
                    item.NoServiceProvided = info.NoServiceProvided;
                    item.NoServiceAvailable = info.NoServiceAvailable;
                    item.NoServiceAccepted = info.NoServiceAccepted;

                }
                if (info.formtype == 7)
                {
                    item.RefFpcenter = info.RefFpcenter;
                    item.Mowadowa = info.Mowadowa;
                    item.Aihrc = info.Aihrc;
                    item.SafeHouse = info.SafeHouse;
                    item.BackHome = info.BackHome;
                    item.Other = info.Other;
                    item.RefNotAvailable = info.RefNotAvailable;
                    item.RefNotAccepted = info.RefNotAccepted;
                    item.OtherSpecify = info.OtherSpecify;
                    item.IsSurvivorWilling = info.IsSurvivorWilling;
                    item.EvidenceCollected = info.EvidenceCollected;
                    item.MedicalCertificate = info.MedicalCertificate;
                    item.Appointment = info.Appointment;
                    item.MedExamProcedure = info.MedExamProcedure;
                    item.ConsentTaken = info.ConsentTaken;
                    item.SpreadInfo = info.SpreadInfo;


                }
                try
                {
                    item.GbvCase.LastUpdate = DateTime.Now;
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntakeInfoExists(info.IntakeInfoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok("success");
            }
            return View(info);
        }

        private bool IntakeInfoExists(int id)
        {
            return _context.IntakeInfo.Any(e => e.IntakeInfoId == id);
        }
    }
}

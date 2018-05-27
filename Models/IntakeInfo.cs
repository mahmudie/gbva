using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rmc.Models
{
    public partial class IntakeInfo
    {
        [NotMapped]
        public int formtype { get; set; }
        public int IntakeInfoId { get; set; }
        public Guid GbvCaseId { get; set; }
        public int? ReferredBy { get; set; }
        public string RefOther { get; set; }
        [Display(Name = "Examination Date")]
        [DataType(DataType.Date)]
        public DateTime? ExamDate { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Examination Time")]
        public TimeSpan? ExamTime { get; set; }

        [Display(Name = "Incident Date")]
        [DataType(DataType.Date)]
        public DateTime? IncDate { get; set; }

        [Display(Name = "Incident Time")]
        [DataType(DataType.Time)]
        public TimeSpan? IncTime { get; set; }
        [Display(Name ="Incident Location")]
        public int? IncLocation { get; set; }
        [Display(Name = "Incident Location Other")]
        public string IncLocationOther { get; set; }
        [Display(Name = "Incident Type")]
        public int? IncType { get; set; }

        public int? IncReportToOther { get; set; }
        public int? Relationship { get; set; }
        [Display(Name = "# of Perpetrators ")]
        public int? NoOfPerpetrators { get; set; }
        [Display(Name = "Perpetrator's occupoation")]
        public int? PerpetratorOccupation { get; set; }

        [Display(Name = "Perpetrator's occupoation other ")]
        public string OccupationOther { get; set; }
        public int? AgeGroup { get; set; }
        public int? StiStatus { get; set; }
        public int? HcvHbs { get; set; }
        [Display(Name = "Evidence Of Pregnancy")]
        public int? EvidenceOfPregnancy { get; set; }
        public int? PregnancyWeeks { get; set; }
        public int? Results { get; set; }
        public string ResultsOther { get; set; }
        [Display(Name = "Psychological Status")]
        public string PsychologicalStatus { get; set; }
        public int? ExaminedForAids { get; set; }
        public int? SstPrevenMed { get; set; }
        public int? MedForInjuries { get; set; }
        public int? VacForTreat { get; set; }
        public int? ReferToPsychosocial { get; set; }
        public int? ReferToHigherMedical { get; set; }
        public bool YesBetterFacilities { get; set; }
        public bool YesFpservices { get; set; }
        public bool YesVaccination { get; set; }
        public bool YesConsulation { get; set; }
        public bool YesVct { get; set; }
        public bool YesPregnancyTest { get; set; }
        public bool YesOther { get; set; }
        public bool NoServiceProvided { get; set; }
        public bool NoServiceAccepted { get; set; }
        public bool NoServiceAvailable { get; set; }
        public bool RefFpcenter { get; set; }
        public bool Mowadowa { get; set; }
        public bool Aihrc { get; set; }
        public bool SafeHouse { get; set; }
        public bool BackHome { get; set; }
        public bool Other { get; set; }
        public string OtherSpecify { get; set; }
        public bool RefNotAvailable { get; set; }
        public bool RefNotAccepted { get; set; }
        public int? IsSurvivorWilling { get; set; }
        public int? EvidenceCollected { get; set; }
        public int? MedicalCertificate { get; set; }
        public int? Appointment { get; set; }
        public int? MedExamProcedure { get; set; }
        public int? ConsentTaken { get; set; }
        public int? SpreadInfo { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? InsertDate { get; set; }

        public virtual GbvCase GbvCase { get; set; }
    }
}

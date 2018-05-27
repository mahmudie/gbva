using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rmc.Models
{
    public partial class Consent
    {
        public int ConsentId { get; set; }
        public Guid GbvCaseId { get; set; }
        [Display(Name = ("Physical Examinations"))]
        public int PhysicalExams { get; set; }
        [Display(Name = ("Pelvic Examinations"))]
        public int PelvicExams { get; set; }
        [Display(Name = ("Speculum Examinations"))]
        public int SpeculumExams { get; set; }
        public int OtherExams { get; set; }
        [Display(Name = ("Blood Transfusion"))]
        public int BloodExams { get; set; }
        public int ProvisionOfInfo { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? InsertDate { get; set; }

        public virtual GbvCase GbvCase { get; set; }
    }
}

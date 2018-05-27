using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rmc.Models
{
    public partial class Registration
    {
        public int RegistrationId { get; set; }
        public Guid GbvCaseId { get; set; }
        [Display(Name="Year")]
        public int RptYear { get; set; }
        [Display(Name = "Month")]
        public int RptMonth { get; set; }
        [Display(Name = "is IDP(Internally displaced person")]
        public int IsIdp { get; set; }
        [Display(Name = "Type Of Violence")]
        public int TypeOfViolence { get; set; }
        [Display(Name = "Gbv History")]
        public int GbvHistory { get; set; }
        [Display(Name = "refer in by")]
        public int? RefIn { get; set; }
        [Display(Name = "refer out to")]
        public int? RefOut { get; set; }
        [Display(Name = "Medical services")]
        public bool ServiceMedical { get; set; }
        [Display(Name = "Psychosocial services")]
        public bool ServicePsycho { get; set; }
        [Display(Name = "Referred to legal services")]
        public bool ServiceRefLegal { get; set; }
        [Display(Name = "Refer to safe house")]
        public bool ServiceRefSafeHouse { get; set; }
        public string Remarks { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? InsertDate { get; set; }

        public virtual GbvCase GbvCase { get; set; }
    }
}

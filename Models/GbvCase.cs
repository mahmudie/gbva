using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rmc.Models
{
    public partial class GbvCase
    {
        public GbvCase()
        {
            Authorization = new HashSet<Authorization>();
            Consent = new HashSet<Consent>();
            IntakeInfo = new HashSet<IntakeInfo>();
            Registration = new HashSet<Registration>();
        }

        public Guid GbvCaseId { get; set; }
        [Display(Name = "INC Code")]
        public string IncCode { get; set; }
        [Required]
        public int? RegNo { get; set; }
        [Display(Name = "ITR Code")]
        [Required]
        public string ItrCode { get; set; }
        [Display(Name = "hospital")]
        public int? HospitalId { get; set; }
        [Display(Name = "patient name")]
        [Required]
        public string PatientName { get; set; }
        [Display(Name = "patient father name")]
        [Required]
        public string PatientFatherName { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public int? Sex { get; set; }
        public string Address { get; set; }
        [Display(Name = "Martial status")]
        [Required]
        public int? MaritalStatus { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? RefDate { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan? RefTime { get; set; }
        public string UserName { get; set; }
        [Display(Name = "Updated at")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LastUpdate { get; set; }
        [Display(Name = "Created at")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? InsertDate { get; set; }

        public virtual ICollection<Authorization> Authorization { get; set; }
        public virtual ICollection<Consent> Consent { get; set; }
        public virtual ICollection<IntakeInfo> IntakeInfo { get; set; }
        public virtual ICollection<Registration> Registration { get; set; }
        public virtual FacilityInfo Hospital { get; set; }
    }
}

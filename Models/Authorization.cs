using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rmc.Models
{
    public partial class Authorization
    {
        public Authorization()
        {
            AuthorizationSub = new HashSet<AuthorizationSub>();
        }

        public int AuthorizationId { get; set; }
        public Guid? GbvCaseId { get; set; }
        public bool Islessthan18 { get; set; }
        public bool IsSigned { get; set; }
        [NotMapped]
        [Required]
        [Display(Name = "Agency Type")]
        public int? AgencyTypeId { get; set; }
        [NotMapped]
        [Display(Name = "Agency Name")]
        public string AgencyName { get; set; }
        [NotMapped]
        public string Comment { get; set; }

        public virtual ICollection<AuthorizationSub> AuthorizationSub { get; set; }
        public virtual GbvCase GbvCase { get; set; }
    }
}

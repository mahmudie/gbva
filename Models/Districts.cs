using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rmc.Models
{
    public partial class Districts
    {
        public Districts()
        {
            FacilityInfo = new HashSet<FacilityInfo>();
        }
        [Display(Name = "District Code")]
        public string DistCode { get; set; }
        [Display(Name = "Created at")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Distric Name")]
        public string DistName { get; set; }
        [Display(Name = "Distric Name Dari")]
        public string DistNameDari { get; set; }
        [Display(Name = "Distric Name pashto")]
        public string DistNamePashto { get; set; }
        [Display(Name = "Province")]
        public string ProvCode { get; set; }

        public virtual ICollection<FacilityInfo> FacilityInfo { get; set; }
        [Display(Name="Province")]
        public virtual Provinces ProvCodeNavigation { get; set; }
    }
}

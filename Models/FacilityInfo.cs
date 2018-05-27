using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rmc.Models
{
    public partial class FacilityInfo
    {
        public FacilityInfo()
        {
            GbvCase = new HashSet<GbvCase>();
        }
        [Required]
        [Display(Name = ("Facility Id"))]
        public int FacilityId { get; set; }
        public string ActiveStatus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateEstablished { get; set; }
        [Display(Name = ("District"))]
        public string DistCode { get; set; }
        [Required]
        [Display(Name = ("Facility Name"))]
        public string FacilityName { get; set; }
        [Display(Name = ("Name Dari"))]
        public string FacilityNameDari { get; set; }
        [Display(Name = ("Name Pashto"))]
        public string FacilityNamePashto { get; set; }
        [Display(Name=("Facility Type"))]
        public int FacilityType { get; set; }
        [Display(Name = ("Gps Lattitude"))]
        public double? Gpslattitude { get; set; }
        [Display(Name = ("Gps Longtitude"))]
        public double? Gpslongtitude { get; set; }
        [Required]
        public string Implementer { get; set; }
        public double? Lat { get; set; }
        public string Location { get; set; }
        [Display(Name = ("Location Dari"))]
        public string LocationDari { get; set; }
        [Display(Name = ("Location Pashto "))]
        public string LocationPashto { get; set; }
        public double? Lon { get; set; }
        public string SubImplementer { get; set; }
        public string ViliCode { get; set; }
        public string User { get; set; }

        public virtual ICollection<GbvCase> GbvCase { get; set; }
        [Display(Name = ("District"))]
        public virtual Districts DistCodeNavigation { get; set; }
    }
}

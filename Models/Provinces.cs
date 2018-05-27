using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rmc.Models
{
    public partial class Provinces
    {
        public Provinces()
        {
            Districts = new HashSet<Districts>();
        }
        [Display(Name="Province Code")]
        public string ProvCode { get; set; }
        [Display(Name = "AGCHO Code")]

        public int? Aghchocode { get; set; }

        [Display(Name = "AIMS Code")]
        public int? Aimscode { get; set; }
        [Display(Name = "Created at")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Name")]
        public string ProvName { get; set; }
        [Display(Name = "Name Dari")]
        public string ProveNameDari { get; set; }
        [Display(Name = "Name Pashto")]
        public string ProveNamePashto { get; set; }

        public virtual ICollection<Districts> Districts { get; set; }
    }
}

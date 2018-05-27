using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rmc.Models
{
    public partial class Tenant
    {   [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]    
        [Display(Name = "Tenant Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
    }
}

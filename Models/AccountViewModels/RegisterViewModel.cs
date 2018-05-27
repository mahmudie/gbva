using System.ComponentModel.DataAnnotations;

namespace rmc.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "User Name")]
        [RegularExpression(@"^\w+$",ErrorMessage="Invalid Input")]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^\w+$",ErrorMessage="Invalid Input")]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^['0-9']*$",ErrorMessage="Invalid Input")]
        public string Number { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^\w+$",ErrorMessage="Invalid Input")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Position { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "User Role")]
        public string Role { get; set; }
        [Required]        
        public int TenantId { get; set; }
    }
}

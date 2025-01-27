using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ResetVM
    {
        public string Email { get; set; }
        [Required(ErrorMessage = "Password field is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[\W_])[\S]{6,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one special character,be at least 6 characters long, and contain no spaces.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password field is required")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CompRegVM
    {
        [Required(ErrorMessage = "name field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "user name filed is required")]
        [RegularExpression(@"^\S+$", ErrorMessage = "user name field must not contain space")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "email field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password field is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[\W_])[\S]{6,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one special character,be at least 6 characters long, and contain no spaces.")]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "date field is required")]
        public DateOnly Date { get; set; }
        [Required(ErrorMessage = "detail field is required")]
        public string Detail { get; set; }
        [Required(ErrorMessage = "address field is required")]
        public string Address { get; set; }
    }
}

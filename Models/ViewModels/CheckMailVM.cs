using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CheckMailVM
    {
        [Required(ErrorMessage = "email field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

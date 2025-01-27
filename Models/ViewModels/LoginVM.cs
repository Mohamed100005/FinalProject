using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "user name or email field is required")]
        public string Account { get; set; }

        [Required(ErrorMessage = "password field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Check { get; set; }
        public List<AuthenticationScheme> ExternalLogins = new List<AuthenticationScheme>();
    }
}

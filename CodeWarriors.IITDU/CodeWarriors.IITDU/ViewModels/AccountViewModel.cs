using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeWarriors.IITDU.ViewModels
{
    public class LoginModel
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        public String Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String UserName { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }
    }
}
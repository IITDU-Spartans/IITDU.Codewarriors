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
    public class ProfileModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        [EmailAddress]
        public String Email { get; set; }
        public String Gender { get; set; }
        public int Age { get; set; }
        public String Country { get; set; }
        public String About { get; set; }
        public string ImageUrl { get; set; }
    }

    public class EditAccountModel
    {
        public String UserName { get; set; }
        [Required]
        public String CurrentPassword { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }
    }
}
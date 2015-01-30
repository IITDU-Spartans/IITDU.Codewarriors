using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CodeWarriors.IITDU.ViewModels
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }
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
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$", ErrorMessage = "Entered phone format is not valid.")]
        public String MobileNumber { get; set; }
        [Required]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }
    }
    public class ProfileModel
    {
        [Required]
        public String FirstName { get; set; }
        
        [Required]
        public String LastName { get; set; }
        [Required]
        public String Gender { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$", ErrorMessage = "Entered phone format is not valid.")]
        public String MobileNumber { get; set; }

        public int Age { get; set; }
        [Required]
        public String Location { get; set; }
        public String About { get; set; }
        public string ImageUrl { get; set; }
    }

    public class EditAccountModel
    {
        public String Email { get; set; }
        [Required]
        public String CurrentPassword { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }
    }
}
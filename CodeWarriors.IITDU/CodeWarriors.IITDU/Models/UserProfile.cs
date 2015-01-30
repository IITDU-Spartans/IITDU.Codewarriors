using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using  System.Data.Entity;
namespace CodeWarriors.IITDU.Models
{
    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Gender { get; set; }
        public int Age { get; set; }
        public String Location { get; set; }
        public String MobileNumber { get; set; }
        public String About { get; set; }
        public String ImageUrl { get; set; }
    }
}
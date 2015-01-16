using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeWarriors.IITDU.Models
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Gender { get; set; }
        public int Age { get; set; }
        public String Country { get; set; }
        public String About { get; set; }
    }
}
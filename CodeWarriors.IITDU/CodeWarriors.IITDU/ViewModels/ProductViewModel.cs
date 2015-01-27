using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeWarriors.IITDU.ViewModels
{
    public class ProductViewModel
    {
        public String ProductName { get; set; }
        public String CategoryName { get; set; }
        public Double Price { get; set; }
        public String Description { get; set; }
        public String ImageUrl { get; set; }
        public int AvailableCount { get; set; }
    }
}
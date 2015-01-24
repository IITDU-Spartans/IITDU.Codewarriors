using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeWarriors.IITDU.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public String ProductName;
        public int Quantity { get; set; }
        public DateTime AddTime { get; set; }
        public double Price { get; set; }
    }
}
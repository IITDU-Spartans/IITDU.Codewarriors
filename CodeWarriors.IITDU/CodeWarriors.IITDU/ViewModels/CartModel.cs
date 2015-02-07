using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeWarriors.IITDU.ViewModels
{
    public class CartModel
    {
        public int SellerId { get; set; }
        public String SellerName { get; set; }
        public List<String> ProductNames{ get; set; }
        public List<int> ProductIds { get; set; }
        public List<String> Reviews { get; set; }
        public int Rating{ get; set; }
    }
}
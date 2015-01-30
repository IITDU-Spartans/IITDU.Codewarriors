using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CodeWarriors.IITDU.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  ProductId { get; set; }
        public int CatagoryId { get; set; }
        public String ProductName { get; set; }
        public Double Price { get; set; }
        public String Description { get; set; }
        public List<String> ImageUrls { get; set; }
        public int WishCount { get; set; }
        public int PurchaseCount { get; set; }
        public int AvailableCount { get; set; }
        public double AverageRate { get; set; }
        public String AvailableSizes { get; set; } 
        public String CatagoryName { get; set; }
        public String SubCatagoryName { get; set; }
        public String AvailableModels { get; set; }
        public String Manufacturer { get; set; }

    }

}
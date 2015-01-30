using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CodeWarriors.IITDU.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public String ImageUrl { get; set; }
    }

}
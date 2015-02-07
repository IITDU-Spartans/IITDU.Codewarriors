using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeWarriors.IITDU.Models
{
    public class ProductOrder
    {
        [Key]
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public bool DeliveryStatus { get; set; }
        public int ProductId { get; set; }
        public int Price;
    }
}
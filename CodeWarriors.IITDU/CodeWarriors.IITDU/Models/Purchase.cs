using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeWarriors.IITDU.Models
{
    public class Purchase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseTime { get; set; }
    }
}
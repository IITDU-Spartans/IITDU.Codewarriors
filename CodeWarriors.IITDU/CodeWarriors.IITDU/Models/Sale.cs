using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeWarriors.IITDU.Models
{
    public class Sale
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime UploadDateTime { get; set; }
    }

}
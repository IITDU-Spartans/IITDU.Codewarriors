using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeWarriors.IITDU.Models
{
    public class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }
        public int RateId { get; set; }
        public String ReviewDescription { get; set; }
        public DateTime ReviewDateTime { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
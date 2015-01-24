using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeWarriors.IITDU.Models
{
    public class WishedProduct
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WishedProductId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
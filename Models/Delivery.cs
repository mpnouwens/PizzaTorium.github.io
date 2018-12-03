using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaTorium_complete.Models
{
    public class Delivery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [DisplayName("Employee Name")]
        public string Name { get; set; }

        [Required]
        public string Area { get; set; }

        [DisplayName("Photo (Optional)")]
        public string Picture { get; set; }
       
    }
}
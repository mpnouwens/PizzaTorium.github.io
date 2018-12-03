using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PizzaTorium_complete.Models
{
    public class Ingredients
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Ingredients")]
        public string Name { get; set; }


        [Required]
        [DisplayName("Cost")]
        [Column("Cost",TypeName = "Money")]
        public decimal Cost { get; set; }

        public bool Checked { get; set; }

    }
}
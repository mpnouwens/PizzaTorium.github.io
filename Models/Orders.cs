using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace PizzaTorium_complete.Models
{
    [Table("Orders")]
    public class Orders
    {


        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        [DisplayName("Customer Email")]
        public string User { get; set; }

        [MaxLength(50)]
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [MaxLength(50)]
        [DisplayName("Delivery Person")]
        public string EmpName { get; set; }


        [MaxLength(50, ErrorMessage = "Address cannot be greater than 50 characters")]
        [DisplayName("Address")]
        [Required]
        public string Address { get; set; }

        [MaxLength(25)]
        [DisplayName("Area")]
        public string Area { get; set; }

        [MaxLength(10, ErrorMessage = "Paymethod cannot be greater than 10 characters")]
        [DisplayName("Method of Payment")]
        public string PayMethod { get; set; }

        [DisplayName("Cost")]
        [Column("dCost", TypeName = "Money")]
        public decimal Cost { get; set; }


        [DisplayName("Date Ordered")]
        [Column(TypeName = "datetime2")]
        public DateTime DateOrdered { get; set; }
    }
}

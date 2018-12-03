using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PizzaTorium_complete.Models
{
    [Table("TblUsers")]
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "Name cannot be greater than 50 characters")]
        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }

        [MaxLength(30, ErrorMessage = "Username cannot be greater than 50 characters")]
        [DisplayName("Username")]
        [Required]
        public string Username { get; set; }

        [MaxLength(50, ErrorMessage = "Address cannot be greater than 50 characters")]
        [DisplayName("Address")]
        [Required]
        public string Address { get; set; }

        [MaxLength(10, ErrorMessage = "Phone Number cannot be greater than 50 characters")]
        [DisplayName("Phone Number")]
        [Required]
        public string Phone { get; set; }

        [MaxLength(30, ErrorMessage = "Favourite Pizza cannot be greater than 50 characters")]
        [DisplayName("Favourite Pizza (Optional)")]
        public string FavPizza { get; set; }

    }
}
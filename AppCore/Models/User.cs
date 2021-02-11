using System;
using System.ComponentModel.DataAnnotations;

namespace AppCore.Models
{
    public class User
    {
        [Key]
        public string Token { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string Name { get; set; }
    }
}

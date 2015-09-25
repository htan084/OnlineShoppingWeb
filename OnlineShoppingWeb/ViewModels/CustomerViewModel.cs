using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingWeb.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "last name not null", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "string only")]
        public string LastName { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Address { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
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
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "last name not null", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "string only")]
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        [Required, StringLength(9, MinimumLength = 4, ErrorMessage = "UserName length should be between 4 and 9")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
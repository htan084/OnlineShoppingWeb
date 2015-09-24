using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingWeb.ViewModels
{
    public class UserViewModel
    {
            [Required, StringLength(12, MinimumLength = 5, ErrorMessage = "UserName length should be between 5 and 12")]
            public string UserName { get; set; }
            [Required, StringLength(12, MinimumLength = 5, ErrorMessage = "UserPassword length should be between 5 and 12")]
            public string Password { get; set; }       
    }
}
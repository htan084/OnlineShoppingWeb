using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShoppingWeb
{
    [Table("Customers")]
    public class Customer
    {
        public int CustomerID { get; set; }        
        public string FirstName { get; set; }       
        public string LastName { get; set; }       
        public string Mobile { get; set; }      
        public string Address { get; set; }      
        public string UserName { get; set; }       
        public string UserPass { get; set; }       
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
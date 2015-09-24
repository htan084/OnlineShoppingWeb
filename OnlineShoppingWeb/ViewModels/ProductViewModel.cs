using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingWeb.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Url { get; set; }
    }
}
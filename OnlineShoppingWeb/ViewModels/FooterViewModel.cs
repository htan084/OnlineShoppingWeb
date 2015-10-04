using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingWeb.ViewModels
{
    public class FooterViewModel
    {
        public string CompanyName { get; set; }
        public int Year { get; set; }
        public FooterViewModel()
        {
            this.CompanyName = "Liu Zhen's online shop";
            this.Year = DateTime.Now.Year;
        }
    }
}
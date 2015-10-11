using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingWeb.ViewModels
{
    public class OrderViewModel
     
    {
        public int OrderId { get; set; }
        public int OrderNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime OrderTime { get; set; }
        public  string Total { get; set; }
        public string CustomerName { get; set; }
        public bool IsChecked { get; set; }
        public bool IsShipped { get; set; }
        public bool IsReceived { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }

    }
}
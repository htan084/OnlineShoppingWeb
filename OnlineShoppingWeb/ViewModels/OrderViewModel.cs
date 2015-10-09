using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingWeb.ViewModels
{
    public class OrderViewModel
     
    {
        public int OrderId { get; set; }
        public int OrderNo { get; set; }
        public System.DateTime OrderTime { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string CustomerName { get; set; }
        public Nullable<bool> IsChecked { get; set; }
        public Nullable<bool> IsShipped { get; set; }
        public Nullable<bool> IsReceived { get; set; }
    }
}
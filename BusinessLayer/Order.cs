//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public Order()
        {
            this.OrderLines = new HashSet<OrderLine>();
        }
    
        public int OrderId { get; set; }
        public int OrderNo { get; set; }
        public System.DateTime OrderTime { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<bool> IsChecked { get; set; }
        public Nullable<bool> IsShipped { get; set; }
        public Nullable<bool> IsReceived { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}

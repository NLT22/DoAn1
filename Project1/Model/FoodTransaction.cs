//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project1.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class FoodTransaction
    {
        public int transaction_id { get; set; }
        public Nullable<int> Food_id { get; set; }
        public string transaction_type { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<System.DateTime> transaction_date { get; set; }
        public string description { get; set; }
        public Nullable<decimal> expense { get; set; }
        public Nullable<int> pig_pen { get; set; }
    
        public virtual Food Food { get; set; }
        public virtual PigPen PigPen { get; set; }
    }
}

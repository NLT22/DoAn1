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
    
    public partial class MonthlyFinancialRecord
    {
        public int record_id { get; set; }
        public System.DateTime record_month { get; set; }
        public Nullable<decimal> pig_sell { get; set; }
        public Nullable<decimal> medicine_cost { get; set; }
        public Nullable<decimal> food_cost { get; set; }
        public Nullable<decimal> employee_cost { get; set; }
        public Nullable<decimal> other_cost { get; set; }
    }
}

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
    
    public partial class Vacine_history
    {
        public int vacine_history_id { get; set; }
        public string vacine_name { get; set; }
        public Nullable<System.DateTime> vacine_date { get; set; }
        public Nullable<int> require_count { get; set; }
        public Nullable<int> vacine_count { get; set; }
        public Nullable<bool> vacine_done { get; set; }
        public Nullable<int> vacine_duration { get; set; }
        public Nullable<System.DateTime> vacine_next_date { get; set; }
        public Nullable<int> pig_id { get; set; }
    
        public virtual Pig Pig { get; set; }
    }
}
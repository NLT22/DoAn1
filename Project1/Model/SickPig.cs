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
    
    public partial class SickPig
    {
        public int sickpig_id { get; set; }
        public Nullable<int> pig_id { get; set; }
        public string disease_name { get; set; }
        public Nullable<bool> disease_done { get; set; }
        public Nullable<System.DateTime> detection_date { get; set; }
        public string description { get; set; }
    
        public virtual Pig Pig { get; set; }
    }
}

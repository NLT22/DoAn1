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
    
    public partial class TaskShift
    {
        public int task_shift_id { get; set; }
        public Nullable<int> task_id { get; set; }
        public Nullable<int> shift_id { get; set; }
        public bool complete { get; set; }
    
        public virtual Shift Shift { get; set; }
        public virtual Task Task { get; set; }
    }
}

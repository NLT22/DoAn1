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
    
    public partial class Pig
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pig()
        {
            this.Events = new HashSet<Event>();
            this.PigMeasurements = new HashSet<PigMeasurement>();
            this.SickPigs = new HashSet<SickPig>();
            this.Vacine_history = new HashSet<Vacine_history>();
        }
    
        public int pig_id { get; set; }
        public string pig_type { get; set; }
        public string gender { get; set; }
        public Nullable<System.DateTime> date_of_birth { get; set; }
        public string origin { get; set; }
        public Nullable<System.DateTime> date_of_arrival { get; set; }
        public string health_status { get; set; }
        public Nullable<int> pen_id { get; set; }
        public Nullable<int> MonthsOld { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PigMeasurement> PigMeasurements { get; set; }
        public virtual PigPen PigPen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SickPig> SickPigs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vacine_history> Vacine_history { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PowCamp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Velocity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Velocity()
        {
            this.x = 0D;
            this.y = 0D;
        }
    
        public int Id { get; set; }
        public double x { get; set; }
        public double y { get; set; }
    
        public virtual GameObject GameObject { get; set; }
    }
}

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
    
    public partial class SaveGame
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int levelCreatedFrom { get; set; }
    
        public virtual Scene Scene { get; set; }
    }
}

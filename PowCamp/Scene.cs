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
    
    public partial class Scene
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Scene()
        {
            this.timeSinceLastPrisonerSpawn = 99999F;
            this.timeToNextPrisonerSpawn = 3F;
            this.bankBalance = 500;
            this.InstantiatedGameObjects = new HashSet<InstantiatedGameObject>();
        }
    
        public int Id { get; set; }
        public float timeSinceLastPrisonerSpawn { get; set; }
        public float timeToNextPrisonerSpawn { get; set; }
        public int bankBalance { get; set; }
        public float timeSinceLastGrant { get; set; }
    
        public virtual SaveGame SaveGame { get; set; }
        public virtual Level Level { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstantiatedGameObject> InstantiatedGameObjects { get; set; }
    }
}

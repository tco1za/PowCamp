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
    
    public partial class GameObject
    {
        public int Id { get; set; }
    
        public virtual GameObjectType GameObjectType { get; set; }
        public virtual InstantiatedGameObject InstantiatedGameObject { get; set; }
        public virtual ScreenCoord ScreenCoord { get; set; }
        public virtual CellCoord CellCoord { get; set; }
        public virtual CurrentAnimation CurrentAnimation { get; set; }
        public virtual Velocity Velocity { get; set; }
        public virtual Acceleration Acceleration { get; set; }
        public virtual Wall Wall { get; set; }
        public virtual PatrolRoute PatrolRoute { get; set; }
        public virtual CellPartition CellPartition { get; set; }
        public virtual Orientation Orientation { get; set; }
        public virtual TargetScreenCoord TargetScreenCoord { get; set; }
        public virtual PrevScreenCoord PrevScreenCoord { get; set; }
        public virtual TargetPathIndex TargetPathIndex { get; set; }
    }
}

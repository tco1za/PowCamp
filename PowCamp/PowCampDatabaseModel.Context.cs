﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PowCampDatabaseModelContainer : DbContext
    {
        public PowCampDatabaseModelContainer()
            : base("name=PowCampDatabaseModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<EntityType> EntityTypes { get; set; }
        public virtual DbSet<InstantiatedEntity> InstantiatedEntities { get; set; }
        public virtual DbSet<ScreenCoords> ScreenCoords { get; set; }
        public virtual DbSet<CellCoords> CellCoords { get; set; }
        public virtual DbSet<Scene> Scenes { get; set; }
        public virtual DbSet<ComponentDependencies> ComponentDependencies { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<SaveGame> SaveGames { get; set; }
    }
}

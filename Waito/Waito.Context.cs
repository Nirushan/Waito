﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Waito
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WaitoEntities : DbContext
    {
        public WaitoEntities()
            : base("name=WaitoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<WaitoDistributor> WaitoDistributors { get; set; }
        public DbSet<WaitoPage> WaitoPages { get; set; }
        public DbSet<WaitoProduct> WaitoProducts { get; set; }
        public DbSet<WaitoRecipe> WaitoRecipes { get; set; }
    }
}
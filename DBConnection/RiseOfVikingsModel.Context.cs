﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBConnection
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RiseOfVikingsEntities : DbContext
    {
        public RiseOfVikingsEntities()
            : base("name=RiseOfVikingsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<About> About { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<DeletedUsers> DeletedUsers { get; set; }
        public DbSet<Forum> Forum { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Subforum> Subforum { get; set; }
        public DbSet<Thread> Thread { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Videos> Videos { get; set; }
    }
}

﻿using EFECORE.Configurations;
using EFECORE.Models;
using Microsoft.EntityFrameworkCore;

namespace EFECORE
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=EFCoreDb;Integrated Security=true");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Blog>().Property(m => m.Url).IsRequired();
            new BlogEntityTypeConfiguration().Configure(modelBuilder.Entity<Blog>());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Blog> Blogs { get; set; }
    }
}

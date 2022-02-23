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
            //modelBuilder.Ignore<Post>();
            //modelBuilder.Entity<Blog>().ToTable("Blogs", b => b.ExcludeFromMigrations());
            //modelBuilder.Entity<Blog>().ToTable("Blogs");
            modelBuilder.Entity<Blog>().ToTable("Blogs","hr");
            modelBuilder.Entity<Blog>().ToView("ViewNameFromDb","hr");
            modelBuilder.HasDefaultSchema("hr");
            modelBuilder.Entity<Blog>().Ignore (b=>b.ADDon);
            modelBuilder.Entity<Blog>().Property(b=>b.Id).HasColumnName("BlogId");
            modelBuilder.Entity<Blog>(b =>
            {
                b.Property(eb => eb.Url).HasColumnType("varchar(100)").HasColumnName("blogurl").HasDefaultValue(" ");
            });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Blog> Blogs { get; set; }
    }
} 

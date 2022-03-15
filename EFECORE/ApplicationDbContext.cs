﻿using EFECORE.Configurations;
using EFECORE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
            modelBuilder.Entity<Blog>().ToTable("Blogs", "hr");
            modelBuilder.Entity<Blog>().ToView("ViewNameFromDb", "hr");
            modelBuilder.HasDefaultSchema("hr");
            modelBuilder.Entity<Blog>().Ignore(b => b.ADDon);
            modelBuilder.Entity<Blog>().Property(b => b.Id).HasColumnName("BlogId");
            modelBuilder.Entity<Blog>().Property(b => b.Url).HasMaxLength(200);
            modelBuilder.Entity<Blog>(b =>
            {
                b.Property(eb => eb.Url).HasColumnType("varchar(100)").HasColumnName("blogurl").HasDefaultValue(" ");
            });

            modelBuilder.Entity<Blog>().Property(b => b.Url).HasComment("the url2 of the Blog Table from fluent api");
            modelBuilder.Entity<Blog>().HasKey(b => b.Id);
            modelBuilder.Entity<Blog>().HasKey(b => b.Id).HasName("Pk-Blog");
            modelBuilder.Entity<Blog>().HasNoKey();
            modelBuilder.Entity<Blog>().HasKey(b => new { b.Rating, b.Url });// to add composit primary key
            modelBuilder.Entity<Blog>().Property(b => b.Rating).HasDefaultValue(5);
            modelBuilder.Entity<Blog>().Property(b => b.CreateOn).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Author>().Property(b => b.DisplayName).HasComputedColumnSql("[FirstName]+','+[LastName]");
            modelBuilder.Entity<Category>().Property(p=>p.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Category>().Property(p=>p.Id).ValueGeneratedOnUpdate();
            //modelBuilder.Entity<Category>().Property(p=>p.Id).ValueGeneratedOnAddOrUpdate();

            // make 1=>1 relationship
            modelBuilder.Entity<Blog>().HasOne(p=>p.BlogImage).WithOne(i=>i.Blog).HasForeignKey<BlogImage>(bi=>bi.BlogId);
            // make 1=>m relationship
            modelBuilder.Entity<Blog>().HasMany(b=>b.Posts).WithOne(p=>p.Blog) ;
            modelBuilder.Entity<Post>().HasOne(p=>p.Blog).WithMany(b=>b.Posts).HasForeignKey(p=>p.BlogId);
            modelBuilder.Entity<Post>().HasOne<Blog>().WithMany().HasForeignKey(p=>p.BlogId).HasConstraintName("FK_Posts_Blog_Test");
            // make 1=>m relationship part2  v26 make link foregin key not primarykey in first table
            modelBuilder.Entity<RecordOfSales>().HasOne(s => s.Car)
                       .WithMany(c => c.SaleHistory).HasForeignKey(rs => rs.CarLicensePlate)
                                   .HasPrincipalKey(c => c.LicensePlate);
            modelBuilder.Entity<RecordOfSales>().HasOne(s => s.Car)
                       .WithMany(c => c.SaleHistory).HasForeignKey(rs => new { rs.CarLicensePlate ,rs.CarState})
                                   .HasPrincipalKey(c => new { c.LicensePlate ,c.State});
            /// many to many
            modelBuilder.Entity<Post>().HasMany(p => p.Tags).WithMany(t => t.Posts).UsingEntity(j => j.ToTable("PostTagsTest"));

            modelBuilder.Entity<Post>().HasMany(p => p.Tags).WithMany(t => t.Posts)
                .UsingEntity<PostTag>(
                                        j => j.HasOne(pt => pt.Tag).WithMany(t => t.postTags).HasForeignKey(pt => pt.TagId),
                                        j => j.HasOne(pt => pt.Post).WithMany(t => t.postTags).HasForeignKey(pt => pt.PostId),
                                        j =>
                                        {
                                            j.Property(pt => pt.AddedOn).HasDefaultValueSql("GETDATE()");
                                            j.HasKey(pt => new { pt.PostId, pt.TagId });
                                        }
                                      );
            /// indirect many to many relationship 
            modelBuilder.Entity<PostTag>().HasKey(t => new { t.PostId, t.TagId });
            modelBuilder.Entity<PostTag>().HasOne(pt=>pt.Post).WithMany(p=>p.postTags).HasForeignKey(pt=>pt.PostId);
            modelBuilder.Entity<PostTag>().HasOne(pt=>pt.Tag).WithMany(p=>p.postTags).HasForeignKey(pt=>pt.TagId);

            modelBuilder.Entity<Blog>().HasIndex(b => b.Url);
            modelBuilder.Entity<Blog>().HasIndex(b => b.Url).IsUnique();

            modelBuilder.Entity<Person>().HasIndex(b => new {b.FirstName,b.LastName});


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
    public class Car
    {
        public int CarId { get; set; }
        public string LicensePlate { get; set; }
        public string State { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public List<RecordOfSales> SaleHistory { get; set; }
    }
    public class RecordOfSales
    {
        public int RecordOfSalesId { get; set; }
        public DateTime DateSold { get; set; }
        public decimal price { get; set; }
        public string CarLicensePlate { get; set; }
        public string CarState { get; set; }
        public Car Car { get; set; }
    }
}

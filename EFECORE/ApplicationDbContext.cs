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
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

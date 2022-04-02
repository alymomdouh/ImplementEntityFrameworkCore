using EFECORE.Configurations;
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
            modelBuilder.Entity<Blog>().HasIndex(b => b.Url).HasDatabaseName("index-name");
            modelBuilder.Entity<Blog>().HasIndex(b => b.Url).HasFilter("[Url] IS NOT NULL");
            // to add index put not add null so the index will accept the null value
            modelBuilder.Entity<Blog>().HasIndex(b => b.Url).HasFilter(null);


            modelBuilder.Entity<Person>().HasIndex(b => new {b.FirstName,b.LastName});
            // to make Sequences  unique list not only for one table but for all tables
            //modelBuilder.HasSequence<int>("OrderNuber");
            //modelBuilder.HasSequence<int>("OrderNuber", schema: "Shared");
            // to make start value for sequence and the step value
            modelBuilder.HasSequence<int>("OrderNuber", schema: "Shared").StartsAt(100).IncrementsBy(10);
            //modelBuilder.Entity<Order>().Property(o => o.OrderNo).HasDefaultValueSql("NEXT VALUE FOR [OrderNuber]");
            modelBuilder.Entity<Order>().Property(o => o.OrderNo).HasDefaultValueSql("NEXT VALUE FOR Shared.OrderNuber"); 
           // modelBuilder.Entity<OrderTest>().Property(o => o.OrderNo).HasDefaultValueSql("NEXT VALUE FOR [OrderNuber]");
            modelBuilder.Entity<OrderTest>().Property(o => o.OrderNo).HasDefaultValueSql("NEXT VALUE FOR Shared.OrderNuber");
            // seed data
            // when use HasData  must add the primary key auto generate with this function 
            modelBuilder.Entity<Blog>().HasData(
                new Blog { Id = 1, Url = "http://sample.com/blogs/sample-1" },
                new Blog { Id = 2, Url = "http://sample.com/blogs/sample-2" },
                new Blog { Id = 3, Url = "http://sample.com/blogs/sample-3" }
                );
            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, BlogId = 1, Title = "Post 1" },
                new Post { Id = 2, BlogId = 1, Title = "Post 2" },
                new Post { Id = 3, BlogId = 1, Title = "Post 3" },
                new Post { Id = 4, BlogId = 2, Title = "Post 4" },
                new Post { Id = 5, BlogId = 2, Title = "Post 5" },
                new Post { Id = 6, BlogId = 2, Title = "Post 6" },
                new Post { Id = 7, BlogId = 3, Title = "Post 7" },
                new Post { Id = 8, BlogId = 3, Title = "Post 8" },
                new Post { Id = 9, BlogId = 3, Title = "Post 9" }
                );
            // if BlogId not found will give the error
            // other way to seed data inside migration file 
                     //modelBuilder.Sql("INSERT INTO Blogs (Id,Url) VALUES (4,'http://sample.com/blogs/sample-4')");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderTest> OrderTests { get; set; }
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

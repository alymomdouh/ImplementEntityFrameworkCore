using EFECORE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFECORE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SeedData();
            var blogInPageOne = GetPagentation(1,10);
            // make entity not track any models
            var context = new ApplicationDbContext();
            // the entity default add tracking 
            var blog= context.Blogs.AsNoTracking().SingleOrDefault();
            blog.Rating = 100;
            context.SaveChanges();
            // remove tracking from entity 
           var blog2= context.Blogs.AsNoTracking().SingleOrDefault();
            blog.Rating = 100;
            context.SaveChanges();
            //change the default 
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;    //QueryTrackingBehavior.TrackAll;
            // get all trackes in context 
            var trackers = context.ChangeTracker.Entries();
            foreach (var tracker in trackers)
            {
                Console.WriteLine($"{tracker.Entity.ToString()} ,{tracker.State.ToString()}, {tracker.CurrentValues}");
            }
            // example v55 entity framework core -- Eager Loading
                var blog3 = context.Blogs.Include(x=>x.BlogImage.Caption).SingleOrDefault(l=>l.Id==1);
                //Eager Loading with nested navigation properties
                var blog32 = context.Blogs.Include(bi=>bi.BlogImage).ThenInclude(x=>x.Blog).SingleOrDefault(l=>l.Id==1);
            // Explicit Loading only load navigation properties when we need it in 2 steps not 1 step like Eager Loading
                var blog41 = context.Blogs.SingleOrDefault(l => l.Id == 1);
                 context.Entry(blog41).Reference(b=>b.BlogImage).Load();// if one property
                 var checkifloaded=context.Entry(blog41).Reference(b=>b.BlogImage).IsLoaded;// bool
                 var checkIsModified = context.Entry(blog41).Reference(b=>b.BlogImage).IsModified;//bool
            // if navigation properties is Icollection or List
            var blog42 = context.Blogs.SingleOrDefault(l => l.Id == 1);
            context.Entry(blog42).Collection(b => b.Posts).Load(); 
            context.Entry(blog42).Collection(b => b.Posts).Query().Where(x=>x.Id==2).ToList();//make query on list navigation properties
            context.Entry(blog42).Collection(b => b.Posts).Query().Count(); 
        }
        // function to seeddata to database 
        public static void SeedData()
        {
            using (var context = new ApplicationDbContext())
            {
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var blog = context.Blogs.FirstOrDefault(b => b.Url == "http://blogs.msdn.com/adonet");
                if (blog == null)
                {
                    context.Blogs.Add(new Blog() { Url = "http://blogs.msdn.com/adonet" });
                } 
                context.SaveChanges();
            }
        }

        public static List<Blog> GetPagentation(int pageNumber , int pageSize)
        {
            var context = new ApplicationDbContext();
            return context.Blogs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}

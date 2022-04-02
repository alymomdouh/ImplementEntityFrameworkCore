using EFECORE.Models;
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

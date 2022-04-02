using EFECORE.Models;
using System;
using System.Linq;

namespace EFECORE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SeedData();
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
    }
}

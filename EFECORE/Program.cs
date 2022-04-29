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
            var blogInPageOne = GetPagentation(1, 10);
            // make entity not track any models
            var context = new ApplicationDbContext();
            // the entity default add tracking 
            var blog = context.Blogs.AsNoTracking().SingleOrDefault();
            blog.Rating = 100;
            context.SaveChanges();
            // remove tracking from entity 
            var blog2 = context.Blogs.AsNoTracking().SingleOrDefault();
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
            var blog3 = context.Blogs.Include(x => x.BlogImage.Caption).SingleOrDefault(l => l.Id == 1);
            //Eager Loading with nested navigation properties
            var blog32 = context.Blogs.Include(bi => bi.BlogImage).ThenInclude(x => x.Blog).SingleOrDefault(l => l.Id == 1);
            // Explicit Loading only load navigation properties when we need it in 2 steps not 1 step like Eager Loading
            var blog41 = context.Blogs.SingleOrDefault(l => l.Id == 1);
            context.Entry(blog41).Reference(b => b.BlogImage).Load();// if one property
            var checkifloaded = context.Entry(blog41).Reference(b => b.BlogImage).IsLoaded;// bool
            var checkIsModified = context.Entry(blog41).Reference(b => b.BlogImage).IsModified;//bool
            // if navigation properties is Icollection or List
            var blog42 = context.Blogs.SingleOrDefault(l => l.Id == 1);
            context.Entry(blog42).Collection(b => b.Posts).Load();
            context.Entry(blog42).Collection(b => b.Posts).Query().Where(x => x.Id == 2).ToList();//make query on list navigation properties
            context.Entry(blog42).Collection(b => b.Posts).Query().Count();
            //v57 entity framework core -- Lazy Loading  in 3 steps
            //1 - Install - Package Microsoft.EntityFrameworkCore.Proxies from NuGet
            //2 - add[optionsBuilder.UseLazyLoadingProxies().UseSqlServer] in any one of 2 places[ApplicationDbContext.OnConfiguring] oR[Startup.ConfiguresServices]
            //3 - make all navigation properties is virtual
            var blog51 = context.Blogs.SingleOrDefault(l => l.Id == 1);
            Console.WriteLine(blog51.Posts.Count);// load it only when call or use it 
            //v58 entity framework core -- Split Queries   (only work with Eager Loading)       sql profiler
            var blog58 = context.Blogs.Include(x => x.BlogImage.Caption).SingleOrDefault(l => l.Id == 1);// in sql profiler query do in 1 step get and join in same time
            var blog582 = context.Blogs.Include(x => x.BlogImage.Caption).AsSplitQuery().SingleOrDefault(l => l.Id == 1);// in sql profiler query do in 2 step1  SelectDataFromFirstTable step2 SelectDataFromSecondTable and join with first table 
            // make one query default when change the default 
            var blog583 = context.Blogs.Include(x => x.BlogImage.Caption).AsSingleQuery().SingleOrDefault(l => l.Id == 1);
            //v60 entity framework core -- Select Data Using SQL Statement or Stored Procedure - Part 1
            var blogs601 = context.Blogs.FromSqlRaw("select * from Blogs").ToList();
            //v62 entity framework core -- Global Query Filters 
            var blogs621 = context.Blogs.ToList();// will apply Global Query Filters
            var blogs622 = context.Blogs.IgnoreQueryFilters().ToList();// willIgnoreQueryFilters Global Query Filters
            //v64 entity framework core -- Update Record(s)
            var blog64 = context.Blogs.FirstOrDefault(l => l.Id == 1);
            blog64.CreateOn = DateTime.Now;
            context.SaveChanges();// firstway by tracking 
            Blog blog642 = new Blog { CreateOn = DateTime.Now, Id = 1, };
            context.Update(blog642);// this will make other values =null 
            context.Entry(blog64).Property(p => p.BlogImage).IsModified = false;
            context.Entry(blog64).Property(p => p.Rating).IsModified = false;
            //3th way to update 
            //context.Entry(currentlyvalueobject).CurrentValues.SetValues(newvaluesobject);
            //65.[Arabic] Entity Framework Core - 65 Remove Record(s)
            var blog65 = context.Blogs.FirstOrDefault(l => l.Id == 1);
            context.Blogs.Remove(blog65);
            context.SaveChanges();
            //remove range
            var blogrange65 = context.Blogs.Where(l => l.Id >= 2).ToList();
            context.Blogs.RemoveRange(blogrange65);
            context.SaveChanges();
            //66.[Arabic] Entity Framework Core - 66 Delete Related Data
            // default when delte primary row delte all child rows that releated to primary row by foreign key
            // to change the default behavour OnModelCreating function
            //67.[Arabic] Entity Framework Core - 67 Transactions
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var blog67 = context.Blogs.FirstOrDefault(l => l.Id == 1);
                context.Blogs.Remove(blog67);
                context.SaveChanges();
                
                var blog672 = context.Blogs.FirstOrDefault(l => l.Id == 1);
                context.Blogs.Remove(blog67);
                context.SaveChanges();
                
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
            /// make savepoint
            using var transaction2 = context.Database.BeginTransaction();
            try
            { 
                context.Blogs.Add(new Blog { CreateOn = DateTime.Now, Id = 10, });
                context.Blogs.Add(new Blog { CreateOn = DateTime.Now, Id = 30, });
                context.SaveChanges();

                transaction2.CreateSavepoint("savepoint1");


                context.Blogs.Add(new Blog { CreateOn = DateTime.Now, Id = 10, });
                context.Blogs.Add(new Blog { CreateOn = DateTime.Now, Id = 30, });
                context.SaveChanges();

                transaction2.Commit();
            }
            catch (Exception)
            {
                transaction2.RollbackToSavepoint("savepoint1");
                transaction2.Commit();// this will save any before savepoint
            }

            //68.[Arabic] Entity Framework Core - 68 Save Data with Sql Statment and Stored Procedures ExecuteSqlRaw
            context.Database.ExecuteSqlRaw("select * from tablename");
            context.Database.ExecuteSqlRaw("exec Sp_name");
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

        public static List<Blog> GetPagentation(int pageNumber, int pageSize)
        {
            var context = new ApplicationDbContext();
            return context.Blogs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}

using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
            : base(@"Server=UNCLE-SCROOGE;Database=JuicyNews;Trusted_Connection=True;")
        {
            Database.SetInitializer<MyDbContext>(new CreateDatabaseIfNotExists<MyDbContext>());
            Database.CreateIfNotExists();
            Users = this.Set<User>();
            Comments = this.Set<Comment>();
            News = this.Set<News>();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<News> News { get; set; }
    }
}

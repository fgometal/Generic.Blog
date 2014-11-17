using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using MundiPagg.Blog.Domain.Entities;

namespace MundiPagg.Blog.Repository.DatabaseContext
{
    public class BlogDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCommentary> Commentaries { get; set; }

        static BlogDBContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BlogDBContext>());
            //Configuration.ValidateOnSaveEnabled = false;
        }

        public virtual void DatabaseDispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasRequired(x => x.User);
            modelBuilder.Entity<Post>().HasMany(x => x.Commentaries);
            modelBuilder.Entity<PostCommentary>().HasRequired(x => x.User);
        }
    }
}

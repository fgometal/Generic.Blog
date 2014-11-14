using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
using MundiPagg.Blog.Domain.Entities;

namespace MundiPagg.Blog.Repository.EFDbContext
{
    public class BlogDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB5_1._Database
{
    public class DataContext : DbContext
    {
        public DataContext() : base("ConnectionStrings") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL
{
    public class Db : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
        public Db(DbContextOptions options) : base(options)
        {
            
        }
    }
}

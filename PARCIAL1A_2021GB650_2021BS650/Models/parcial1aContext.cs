using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A_2021GB650_2021BS650.Models
{
    public class parcial1aContext : DbContext
    {
        public parcial1aContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<autores> autores{ get; set; }
        public DbSet<autorlibro> autorlibro { get; set; }

        public DbSet<libros> libros { get; set; }
        public DbSet<posts> posts { get; set; }

    }
}

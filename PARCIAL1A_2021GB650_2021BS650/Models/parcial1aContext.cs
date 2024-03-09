using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A_2021GB650_2021BS650.Models
{
    public class parcial1aContext : DbContext
    {
        public parcial1aContext(DbContextOptions options) : base(options)
        {
        }
    }
}

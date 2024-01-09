using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
		

		public DbSet<Araba> Arabas { get; set; }

		public DbSet<Marka> Markas { get; set; }

        public DbSet<İletisim> iletisims { get; set; } 



        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}

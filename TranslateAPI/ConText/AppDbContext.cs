using Microsoft.EntityFrameworkCore;
using TranslateAPI.Entities;

namespace TranslateAPI.ConText
{
    public class AppDbContext:DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Translate> Translates { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuider)
        {
            optionBuider.UseSqlServer("Server=DESKTOP-5BTSQ2P\\SQLEXPRESS;database=TranslateAPI;integrated security=sspi;");
        }
    }
}

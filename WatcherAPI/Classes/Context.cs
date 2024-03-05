using Microsoft.EntityFrameworkCore;

namespace WatcherAPI.Classes
{
    public class Context:DbContext
    {

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=AS-TASKIN63;Database=WatchDb;User Id=sa;Password=1q2w3e4r+!;TrustServerCertificate=True");
        }

        public DbSet<Admin> Admins {  get; set; }
        public DbSet<MachineInfo> Machines { get; set; }

    }
}

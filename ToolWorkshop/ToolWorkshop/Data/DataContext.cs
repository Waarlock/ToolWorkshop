using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToolWorkshop.Data.Entities;

namespace ToolWorkshop.Data
{
    public class DataContext : IdentityDbContext<User>

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Catalog> catalogs { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Movement> movements { get; set; }
        public DbSet<Temporal_Movement> temporal_movements { get; set; }
        public DbSet<Movement_Detail> movement_Details { get; set; }
        public DbSet<Planogram> planograms { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<State> states { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<Tool> tools { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Warehouse> warehouses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movement_Detail>().HasOne(e => e.Temporal_MovementId).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Movement_Detail>().HasOne(e => e.MovementId).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        }
    }
}

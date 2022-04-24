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


        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Planogram> Planograms { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Movement_Detail> Movement_Details { get; set; }
        public DbSet<Temporal_Movement> Temporal_movements { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        }
    }
}
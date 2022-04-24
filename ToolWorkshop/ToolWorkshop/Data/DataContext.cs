using Microsoft.AspNetCore.Identity;
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
        
        
       //public DbSet<Role> Roles { get; set; }
       
        //public DbSet<User> Users { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User", schema: schema);
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role", schema: schema);
            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("User_Role", schema);
            });*/

           /* modelBuilder.Entity<Tool>()
               .HasMany(t => t.Categories)
               .WithMany(c => c.Tools).LeftNavigation.SetForeignKey(t => )
               .UsingEntity<Category_Tool>();
            modelBuilder.Entity<User>()
               .HasMany(u => u.Roles)
               .WithMany(r => r.Users)
               .UsingEntity<Role_User>();*/


            //modelBuilder.Entity<Movement_Detail>().HasOne(e => e.Temporal_MovementId).WithMany().OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<Movement_Detail>().HasOne(e => e.MovementId).WithMany().OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<Movement_Detail>().HasOne(e => e.CatalogId).WithMany().OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            //modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        }
    }
}
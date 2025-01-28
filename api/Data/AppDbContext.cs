using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; } = null!;
        public DbSet<Hardware> Hardware { get; set; } = null!;
        public DbSet<Notification> Notification { get; set; } = null!;
        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<HardwareCategory> HardwareCategory { get; set; } = null!;
        public DbSet<Models.Type> Types { get; set; } = null!;
        public DbSet<HardwareStatus> HardwareStatus { get; set; } = null!;
        public DbSet<Role> Role {  get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Existing relationships

            modelBuilder.Entity<HardwareCategory>()
                .HasOne(hc => hc.user)
                .WithMany()
                .HasForeignKey(hc => hc.userid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HardwareCategory>()
                .HasOne(hc => hc.hardware)
                .WithMany()
                .HasForeignKey(hc => hc.hardwareid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HardwareCategory>()
                .HasOne(hc => hc.category)
                .WithMany()
                .HasForeignKey(hc => hc.categoryid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hardware>()
                .HasOne(h => h.type)
                .WithMany()
                .HasForeignKey(h => h.typeid)
                .OnDelete(DeleteBehavior.Cascade);

            // Add the foreign key relationship between Hardware and HardwareStatus
            modelBuilder.Entity<Hardware>()
                .HasOne(h => h.hardwarestatus)  // Reference the HardwareStatus navigation property
                .WithMany()  // You can change this if HardwareStatus has a collection of Hardware, but here it's assumed there isn't.
                .HasForeignKey(h => h.hardwarestatusid)  // The foreign key property in Hardware model
                .OnDelete(DeleteBehavior.Cascade); // Define the delete behavior (optional)

            // Convert enums if needed
            modelBuilder.Entity<User>()
                .Property(u => u.roleid)
                .HasConversion<int>();

            modelBuilder.Entity<Hardware>()
                .Property(h => h.hardwarestatusid)
                .HasConversion<int>();

            // Table configurations
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Hardware>().ToTable("Hardware");
            modelBuilder.Entity<Notification>().ToTable("Notification");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<HardwareCategory>().ToTable("HardwareCategory");
            modelBuilder.Entity<Models.Type>().ToTable("Type");

            base.OnModelCreating(modelBuilder);
        }

    }
}

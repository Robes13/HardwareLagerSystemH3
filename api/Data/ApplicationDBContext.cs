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
        public DbSet<Role> Role { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure HardwareCategory relationships
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

            // Configure Hardware relationships
            modelBuilder.Entity<Hardware>()
                .HasOne(h => h.type)
                .WithMany()
                .HasForeignKey(h => h.typeid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hardware>()
                .HasOne(h => h.hardwarestatus)
                .WithMany()
                .HasForeignKey(h => h.hardwarestatusid)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure User -> Role relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role) // Navigation property in User
                .WithMany(r => r.users) // Navigation property in Role
                .HasForeignKey(u => u.roleid) // Foreign key in User
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade deletion

            // Convert enums or specific properties if needed
            modelBuilder.Entity<User>()
                .Property(u => u.roleid)
                .HasConversion<int>();

            modelBuilder.Entity<Hardware>()
                .Property(h => h.hardwarestatusid)
                .HasConversion<int>();
            modelBuilder.Entity<User>()
                .Property(u => u.datedeleted)
                .HasColumnType("DATETIME")
                .IsRequired(false);


            // Table configurations
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Hardware>().ToTable("Hardware");
            modelBuilder.Entity<Notification>().ToTable("Notification");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<HardwareCategory>().ToTable("HardwareCategory");
            modelBuilder.Entity<Models.Type>().ToTable("Type");

            base.OnModelCreating(modelBuilder);
        }
    }
}

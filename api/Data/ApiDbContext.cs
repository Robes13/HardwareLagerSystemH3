using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Hardware> Hardware { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<HardwareCategory> HardwareCategory { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<HardwareStatus> HardwareStatus { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<UserHardware> UserHardware { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>().HasIndex(e => e.EmailAddress).IsUnique();

            modelBuilder.Entity<UserHardware>()
                .HasOne(uh => uh.User)
                .WithMany()
                .HasForeignKey(uh => uh.userid);

            modelBuilder.Entity<UserHardware>()
                .HasOne(uh => uh.Hardware)
                .WithMany()
                .HasForeignKey(uh => uh.hardwareid);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.user)
                .WithMany(uh => uh.notifications)
                .HasForeignKey(n => n.userid);
            // Define the relationship between HardwareCategory and Hardware
            modelBuilder.Entity<HardwareCategory>()
                .HasOne(hc => hc.hardware)  // Each HardwareCategory has one Hardware
                .WithMany(h => h.HardwareCategories)  // Each Hardware has many HardwareCategories
                .HasForeignKey(hc => hc.hardwareid)  // Specify the foreign key
                .OnDelete(DeleteBehavior.Cascade);  // Specify the delete behavior

            modelBuilder.Entity<HardwareCategory>()
                .HasOne(hc => hc.category)  // Each HardwareCategory has one Category
                .WithMany()  // Category can have many HardwareCategories
                .HasForeignKey(hc => hc.categoryid)  // Specify the foreign key
                .OnDelete(DeleteBehavior.Cascade);  // Specify the delete behavior

            // Define the relationship between Hardware and other entities
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

            // Table configurations
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Hardware>().ToTable("Hardware");
            modelBuilder.Entity<Notification>().ToTable("Notification");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<HardwareCategory>().ToTable("HardwareCategory");
            modelBuilder.Entity<Types>().ToTable("Type");
            modelBuilder.Entity<UserHardware>().ToTable("UserHardware");

            base.OnModelCreating(modelBuilder);
        }

    }
}
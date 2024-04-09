using Microsoft.EntityFrameworkCore;
using userManagementBack.Models.Domain;

namespace userManagementBack.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<RoleData> RoleDatas { get; set; }
        public DbSet<PermissionData> PermissionDatas { get; set; }
        public DbSet<BridgeUserPermissionData> UserPermissionDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UserData>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserData>()
                .HasMany(u => u.Permissions)
                .WithOne(r => r.UserData)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<PermissionData>()
                .HasMany(u => u.Users)
                .WithOne(r => r.PermissionData)
                .HasForeignKey(ur => ur.PermissionId);
            
            modelBuilder.Entity<BridgeUserPermissionData>()
                .Property(u => u.UserPermissionId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BridgeUserPermissionData>()
                .ToTable("UserPermissionDatas");
        }
    }
}
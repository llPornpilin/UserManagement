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
    }
}
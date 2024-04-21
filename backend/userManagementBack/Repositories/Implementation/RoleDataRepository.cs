using Microsoft.EntityFrameworkCore;
using userManagementBack.Data;
using userManagementBack.Models.Domain;
using userManagementBack.Repositories.Interface;

namespace userManagementBack.Repositories.Implementation
{
    public class RoleDataRepository : IRoleDataRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RoleDataRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<RoleData>> GetRoleDataAsync() {
            var returnRoleData = await dbContext.RoleDatas.ToListAsync();
            return returnRoleData;
        }
        
    }
}
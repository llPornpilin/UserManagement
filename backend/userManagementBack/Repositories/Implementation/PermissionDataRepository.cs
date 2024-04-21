using Microsoft.EntityFrameworkCore;
using userManagementBack.Data;
using userManagementBack.Models.Domain;
using userManagementBack.Repositories.Interface;

namespace userManagementBack.Repositories.Implementation
{
    public class PermissionDataRepository : IPermissionDataRepository
    {
        public ApplicationDbContext dbContext { get; }
        public PermissionDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<PermissionData>> GetPermissionDataAsync()
        {
            var returnPermission = await dbContext.PermissionDatas.ToListAsync();
            return returnPermission;
        }

    }
}
using Microsoft.EntityFrameworkCore;
using userManagementBack.Data;
using userManagementBack.Models.Domain;
using userManagementBack.Repositories.Interface;

namespace userManagementBack.Repositories.Implementation
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }
        public async Task<UserData> CreateAsync(UserData userData)
        {
            await dbContext.UserDatas.AddAsync(userData);
            await dbContext.SaveChangesAsync();

            return userData;
        }

        public async Task<IEnumerable<UserData>> GetAllAsync(int pageNumber, int pageSize, string search, string orderBy, string orderDirection)
        {
            var returnUserData = await dbContext.UserDatas.ToListAsync();
            return returnUserData;
        }

        public async Task<UserData?> UpdateAsync(UserData user, IList<BridgeUserPermissionData> bridgeUserPermission)
        {
            var existingUser = await dbContext.UserDatas.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser != null) {
                dbContext.Entry(existingUser).CurrentValues.SetValues(user);

                var existingPermissions = dbContext.UserPermissionDatas.Where(p => p.UserId == user.Id);
                dbContext.UserPermissionDatas.RemoveRange(existingPermissions);

                // Update permissions (using AddRange)
                var newPermissions = bridgeUserPermission.Select(permission => new BridgeUserPermissionData
                {
                    UserId = user.Id,
                    PermissionId = permission.PermissionId,
                    IsReadable = permission.IsReadable,
                    IsWritable = permission.IsWritable,
                    IsDeletable = permission.IsDeletable
                });
                dbContext.UserPermissionDatas.AddRange(newPermissions);

                await dbContext.SaveChangesAsync();
                return user;
            }
            return null;
        }

        public async Task<UserData?> DeleteAsync(string userId)
        {
            var existingUser = await dbContext.UserDatas
                .Include(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (existingUser is null) {
                return null;
            }

            if (existingUser.Permissions != null) {
                dbContext.UserPermissionDatas.RemoveRange(existingUser.Permissions);
            }
            

            dbContext.UserDatas.Remove(existingUser);
            await dbContext.SaveChangesAsync();

            return existingUser;
        }
    }
}
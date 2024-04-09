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

        public async Task<UserData?> UpdateAsync(UserData user)
        {
            var existingUser = await dbContext.UserDatas.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser != null) {
                dbContext.Entry(existingUser).CurrentValues.SetValues(user);
                await dbContext.SaveChangesAsync();
                return user;
            }
            return null;

            // var existingUser = await dbContext.UserDatas.Include(u => u.Permissions).FirstOrDefaultAsync();
            // if (existingUser != null) {
            //     existingUser.Id = user.Id;
            //     existingUser.FirstName = user.FirstName;
            //     existingUser.LastName = user.LastName;
            //     existingUser.Email = user.Email;
            //     existingUser.Phone = user.Phone;
            //     existingUser.RoleId = user.RoleId;
            //     existingUser.Role = user.Role;
            //     existingUser.UserName = user.UserName;
            //     existingUser.Password = user.Password;
                
            //     if (user.Permissions != null && user.Permissions.Any()) {
            //         existingUser.Permissions?.ToList().AddRange(user.Permissions.Select(p => new BridgeUserPermissionData {
            //             UserPermissionId = p.UserPermissionId,
            //             IsReadable = p.IsReadable,
            //             IsWritable = p.IsWritable,
            //             IsDeletable = p.IsDeletable
            //         }));
                    
            //     }
            //     dbContext.UserDatas.Update(existingUser);
            //     await dbContext.SaveChangesAsync();

            //     return existingUser;
            // }

            // return null;
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
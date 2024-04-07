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

        public Task<IEnumerable<UserData>> DeleteAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserData>> EditAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
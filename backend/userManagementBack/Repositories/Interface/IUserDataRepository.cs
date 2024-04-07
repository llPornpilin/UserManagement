using userManagementBack.Models.Domain;

namespace userManagementBack.Repositories.Interface
{
    public interface IUserDataRepository
    {
        Task<UserData> CreateAsync(UserData userData);
        Task<IEnumerable<UserData>> GetAllAsync(int pageNuumber, int pageSize, string search, string orderBy, string orderDirection);
        // Task<IEnumerable<UserData>> EditAsync(string userId);
        // Task<IEnumerable<UserData>> DeleteAsync(string userId);
    }
}
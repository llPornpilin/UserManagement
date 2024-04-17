using userManagementBack.Models.Domain;

namespace userManagementBack.Repositories.Interface
{
    public interface IUserDataRepository
    {
        Task<UserData> CreateAsync(UserData userData);
        Task<IEnumerable<UserData>> GetAllAsync(int pageNuumber, int pageSize, string search, string orderBy, string orderDirection);
        Task<UserData?> UpdateAsync(UserData user, IList<BridgeUserPermissionData> bridgeUserPermission);
        Task<UserData?> DeleteAsync(string userId);
        Task<UserData?> GetById(string id);
    }
}
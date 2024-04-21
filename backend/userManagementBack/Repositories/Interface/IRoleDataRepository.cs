using userManagementBack.Models.Domain;

namespace userManagementBack.Repositories.Interface
{
    public interface IRoleDataRepository
    {
        Task<IEnumerable<RoleData>> GetRoleDataAsync();
    }
}
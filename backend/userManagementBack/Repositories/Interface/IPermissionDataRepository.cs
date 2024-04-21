using userManagementBack.Models.Domain;

namespace userManagementBack.Repositories.Interface
{
    public interface IPermissionDataRepository
    {
        Task<IEnumerable<PermissionData>> GetPermissionDataAsync();
    }
}
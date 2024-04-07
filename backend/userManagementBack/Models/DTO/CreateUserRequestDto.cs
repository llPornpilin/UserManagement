using userManagementBack.Models.Domain;

namespace userManagementBack.Models.DTO
{
    public class CreateUserRequestDto
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string RoleId { get; set; } = string.Empty;
        public RoleData? Role { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<BridgeUserPermissionData>? Permissions { get; set; }
    }
}
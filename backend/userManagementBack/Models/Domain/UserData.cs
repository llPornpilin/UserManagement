using System.ComponentModel.DataAnnotations;

namespace userManagementBack.Models.Domain
{
    public class UserData
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string RoleId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<PermissionData>? Permissions { get; set; }
        public string CreatedDate { get; set; } = string.Empty;
    }
}
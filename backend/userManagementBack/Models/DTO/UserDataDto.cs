using userManagementBack.Models.Domain;

namespace userManagementBack.Models.DTO
{
    public class UserDataDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<PermissionData> Permissions { get; set; }
        public string CreatedDate { get; set; }
    }
}
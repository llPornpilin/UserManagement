using System.ComponentModel.DataAnnotations;

namespace userManagementBack.Models.Domain
{
    public class BridgeUserPermissionData
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public UserData? UserData { get; set; }
        public string PermissionId { get; set; } = string.Empty;
        public PermissionData? PermissionData { get; set; }
    }
}
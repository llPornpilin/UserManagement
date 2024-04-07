using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace userManagementBack.Models.Domain
{
    public class PermissionData
    {
        [Key]
        public string PermissionId { get; set; } = string.Empty;
        public string PermissionName { get; set; } = string.Empty;
        public ICollection<BridgeUserPermissionData>? Users { get; set; }
    }
}
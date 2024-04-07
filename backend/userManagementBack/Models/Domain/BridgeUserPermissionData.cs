using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace userManagementBack.Models.Domain
{
    public class BridgeUserPermissionData
    {
        [Key]
        public int UserPermissionId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public UserData? UserData { get; set; }
        
        public string PermissionId { get; set; } = string.Empty;
        public PermissionData? PermissionData { get; set; }
        public bool IsReadable { get; set; }
        public bool IsWritable { get; set; }
        public bool IsDeletable { get; set; }
    }
}
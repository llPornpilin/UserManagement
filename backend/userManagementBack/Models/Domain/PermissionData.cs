using System.ComponentModel.DataAnnotations;

namespace userManagementBack.Models.Domain
{
    public class PermissionData
    {
        [Key]
        public string PermissionId { get; set; } = string.Empty;
        public string PermissionName { get; set; } = string.Empty;
        public bool IsReadable { get; set; }
        public bool IsWritable { get; set; }
        public bool IsDeletable { get; set; }
    }
}
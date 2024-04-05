using System.ComponentModel.DataAnnotations;

namespace userManagementBack.Models.Domain
{
    public class RoleData
    {
        [Key]
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
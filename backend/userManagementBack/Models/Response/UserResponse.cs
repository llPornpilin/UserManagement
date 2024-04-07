using userManagementBack.Models.DTO;

namespace userManagementBack.Models.Response
{
    public class UserResponse
    {
        public Status Status { get; set; } = new Status();
        public UserDataDto Data { get; set; } = new UserDataDto();
    }

    public class Status
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
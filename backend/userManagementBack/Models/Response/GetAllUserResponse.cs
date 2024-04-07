using userManagementBack.Models.DTO;

namespace userManagementBack.Models.Response
{
    public class GetAllUserResponse
    {
        public UserDataDto DataSource { get; set; } = new UserDataDto();
        public int Page { get; set; }
        public int PageSize { get; set;}
        public int TotalCount { get; set; }
    }
}
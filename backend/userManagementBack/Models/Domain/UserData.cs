namespace userManagementBack.Models.Domain
{
    public class UserData
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Array Permission { get; set; }
    }
}
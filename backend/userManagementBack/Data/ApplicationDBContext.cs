using Microsoft.EntityFrameworkCore;

namespace userManagementBack.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

    }
}
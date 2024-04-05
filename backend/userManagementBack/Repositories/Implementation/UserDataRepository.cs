using Microsoft.EntityFrameworkCore;
using userManagementBack.Data;
using userManagementBack.Models.Domain;
using userManagementBack.Repositories.Interface;

namespace userManagementBack.Repositories.Implementation
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public UserDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }
        public async Task<UserData> CreateAsync(UserData userData)
        {
            await dbContext.UserDatas.AddAsync(userData);
            await dbContext.SaveChangesAsync();

            return userData;
        }

        public async Task<IEnumerable<UserData>> GetAllAsync(int pageNumber, int pageSize, string search, string orderBy, string orderDirection)
        {
            var returnUserData = dbContext.UserDatas.AsQueryable();
            search = string.IsNullOrEmpty(search) ? "" : search.ToLower();
            orderBy = string.IsNullOrEmpty(orderBy) ? "" : orderBy.ToLower();

            if (search != "") {
                returnUserData = returnUserData.Where(userData => userData.FirstName.ToLower().Contains(search) || 
                                                        userData.LastName.ToLower().Contains(search) || 
                                                        userData.Email.ToLower().Contains(search));
            }

            if (orderBy != "") {
                switch (orderBy) {
                    case "name":
                        returnUserData = orderDirection == "asc" ?
                            returnUserData.OrderBy(a => a.FirstName) :
                            returnUserData.OrderByDescending(a => a.FirstName);
                            Console.WriteLine("____NO EMPTY, NAME CASE");
                        break;
                    case "role": // TODO: change Role to object and access to role name
                        returnUserData = orderDirection == "asc" ?
                            returnUserData.OrderBy(a => a.RoleId) :
                            returnUserData.OrderByDescending(a => a.RoleId);
                            Console.WriteLine("____NO EMPTY, ROLE CASE");
                        break;
                    case "createddate":
                        returnUserData = orderDirection == "asc" ? 
                            returnUserData.OrderBy(a => a.CreatedDate) :
                            returnUserData.OrderByDescending(a => a.CreatedDate);
                            Console.WriteLine("____NO EMPTY, DATE CASE");
                        break;
                    default:
                        returnUserData = returnUserData.OrderBy(a => a.CreatedDate);
                        Console.WriteLine("____NO EMPTY, DEFAULT CASE");
                        break;
                }
            }

            var totalCount = await returnUserData.CountAsync();
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var userPerPage = totalCount == 0 ? Enumerable.Empty<UserData>() : returnUserData.ToList().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return userPerPage;
        }
    }
}
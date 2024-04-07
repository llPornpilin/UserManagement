using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using userManagementBack.Data;
using userManagementBack.Models.Domain;
using userManagementBack.Models.DTO;
using userManagementBack.Models.Response;
using userManagementBack.Repositories.Interface;

namespace userManagementBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserDataRepository userDataRepository;

        public UserController(IUserDataRepository userDataRepository)
        {
            this.userDataRepository = userDataRepository;

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequestDto request) {

            // Map DTO to Domain model
            var newUser = new UserData {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                RoleId = request.RoleId,
                UserName = request.UserName,
                Password = request.Password,
                Permissions = request.Permissions?.Select(p => new BridgeUserPermissionData {
                    PermissionId = p.PermissionId,
                    IsReadable = p.IsReadable,
                    IsWritable = p.IsWritable,
                    IsDeletable = p.IsDeletable
                }).ToList(),
                CreatedDate = DateTime.UtcNow.ToString("dd MMMM, yyyy", CultureInfo.InvariantCulture)
            };

            await userDataRepository.CreateAsync(newUser);

            var response = new UserResponse(); // TODO: change response
            response.Status.Code = "Success";
            response.Status.Description = "User created successfully .";
            response.Data.Id = newUser.Id;
            response.Data.FirstName = newUser.FirstName;
            response.Data.LastName = newUser.LastName;
            response.Data.Email = newUser.Email;
            response.Data.Phone = newUser.Phone;
            response.Data.Role = newUser.Role;
            response.Data.UserName = newUser.UserName;
            
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int pageNumber = 1, int pageSize = 10, string search = "", string orderBy = "", string orderDirection = "") {

            var allUser = await userDataRepository.GetAllAsync(pageNumber, pageSize, search, orderBy, orderDirection);

            // Map domain model to DTO
            var response = new
            {
                datasource = allUser.Select(user => new
                {
                    userId = user.Id.ToString(),
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                    role = user.Role,
                    userName = user.UserName,
                    permission = user?.Permissions?.Select(p => new {
                        p.PermissionId,
                    }),
                    createdDate = user?.CreatedDate
                }),
                pageNumber = pageNumber,
                pageSize = pageSize,
                totalCount = allUser.Count()
            };

            // var response = new GetAllUserResponse(); // TODO: change response
            // response.Page = pageNumber;
            // response.PageSize = pageSize;
            // response.TotalCount = allUser.Count();

            return Ok(response);
        }

        // [HttpPost]
        // public async Task<IActionResult> EditUser() {

        // }
    }
}
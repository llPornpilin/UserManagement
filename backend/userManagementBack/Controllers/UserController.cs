using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using userManagementBack.Data;
using userManagementBack.Models.Domain;
using userManagementBack.Models.DTO;
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
                Permissions = request.Permissions.Select(p => new PermissionData {
                    PermissionId = p.PermissionId,
                    PermissionName = p.PermissionName,
                    IsReadable = p.IsReadable,
                    IsWritable = p.IsWritable,
                    IsDeletable = p.IsDeletable
                }).ToList(),
                CreatedDate = DateTime.UtcNow.ToString("dd MMMM, yyyy", CultureInfo.InvariantCulture)
            };

            await userDataRepository.CreateAsync(newUser);


            // Damain model to DTO
            var response = new UserDataDto {
                Id = newUser.Id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Phone = newUser.Phone,
                RoleId = newUser.RoleId,
                UserName = newUser.UserName,
                Password = newUser.Password,
                Permissions = request.Permissions.Select(p => new PermissionData {
                    PermissionId = p.PermissionId,
                    PermissionName = p.PermissionName,
                    IsReadable = p.IsReadable,
                    IsWritable = p.IsWritable,
                    IsDeletable = p.IsDeletable
                }).ToList(),
                CreatedDate = newUser.CreatedDate
            };

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
                    userId = user.Id.ToString(), // Ensure ID conversion to string
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                    roleId = user.RoleId,
                    userName = user.UserName,
                    permissions = user.Permissions,
                    createdDate = user.CreatedDate
                }),
                pageNumber = pageNumber,
                pageSize = pageSize,
                totalCount = allUser.Count()
            };

            return Ok(response);
        }
    }
}
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

            search = string.IsNullOrEmpty(search) ? "" : search.ToLower();
            orderBy = string.IsNullOrEmpty(orderBy) ? "" : orderBy.ToLower();

            if (search != "") {
                allUser = allUser.Where(userData => userData.FirstName.ToLower().Contains(search) || 
                                                        userData.LastName.ToLower().Contains(search) || 
                                                        userData.Email.ToLower().Contains(search));
            }

            if (orderBy != "") {
                switch (orderBy) {
                    case "name":
                        allUser = orderDirection == "asc" ?
                            allUser.OrderBy(a => a.FirstName) :
                            allUser.OrderByDescending(a => a.FirstName);
                        break;
                    // case "role": // TODO: change Role to object and access to role name
                    //     allUser = orderDirection == "asc" ?
                    //         allUser.OrderBy(a => a.RoleId) :
                    //         allUser.OrderByDescending(a => a.RoleId);
                    //     break;
                    case "createddate":
                        allUser = orderDirection == "asc" ? 
                            allUser.OrderBy(a => a.CreatedDate) :
                            allUser.OrderByDescending(a => a.CreatedDate);
                        break;
                    default:
                        allUser = allUser.OrderBy(a => a.CreatedDate);
                        break;
                }
            }

            var totalCount = allUser.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var userPerPage = totalCount == 0 ? Enumerable.Empty<UserData>() : allUser.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // Map domain model to DTO
            var response = new
            {
                datasource = userPerPage.Select(user => new
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

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, UpdateUserRequestDto request) {
            // Convert DTO to Domain
            var updatedUserData = new UserData {
                Id = id,
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
            
            updatedUserData = await userDataRepository.UpdateAsync(updatedUserData);

            if (updatedUserData == null) {
                return NotFound();
            }

            // Convert Domain to DTO
            var response = new UserResponse(); // TODO: change response
            response.Status.Code = "Success";
            response.Status.Description = "User updated successfully .";
            response.Data.Id = updatedUserData.Id;
            response.Data.FirstName = updatedUserData.FirstName;
            response.Data.LastName = updatedUserData.LastName;
            response.Data.Email = updatedUserData.Email;
            response.Data.Phone = updatedUserData.Phone;
            response.Data.Role = updatedUserData.Role;
            response.Data.UserName = updatedUserData.UserName;

            return Ok(response);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] string id) {
            var deletedUser = await userDataRepository.DeleteAsync(id);
            if (deletedUser is null) {
                return NotFound();
            }

            var response = new UserResponse();
            response.Status.Code = "Success";
            response.Status.Description = "User deleted successfully .";
            response.Data.Id = id;
            response.Data.FirstName = deletedUser.FirstName;
            response.Data.LastName = deletedUser.LastName;
            response.Data.Email = deletedUser.Email;
            response.Data.Phone = deletedUser.Phone;
            response.Data.Role = deletedUser.Role;
            response.Data.UserName = deletedUser.UserName;

            return Ok(response);
        }
    }
}
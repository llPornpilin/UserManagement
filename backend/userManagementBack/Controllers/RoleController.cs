
using Microsoft.AspNetCore.Mvc;
using userManagementBack.Models.Domain;
using userManagementBack.Repositories.Interface;

namespace userManagementBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleDataRepository roleDataRepository;

        public RoleController(IRoleDataRepository roleDataRepository)
        {
            this.roleDataRepository = roleDataRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetRole() {
            var allRole = await roleDataRepository.GetRoleDataAsync();
            var response = new {
                status = new {
                    code = "success",
                    description = "Get Role"
                },
                data = allRole.Select(r => new {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName
                })
            };
            return Ok(response);
        }

    }
}
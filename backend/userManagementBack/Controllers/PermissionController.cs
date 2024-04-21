using Microsoft.AspNetCore.Mvc;
using userManagementBack.Repositories.Interface;

namespace userManagementBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        public IPermissionDataRepository PermissionDataRepository { get; }
        public PermissionController(IPermissionDataRepository permissionDataRepository) {
            this.PermissionDataRepository = permissionDataRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPermission() {
            var permission = await PermissionDataRepository.GetPermissionDataAsync();
            var response = new {
                status = new {
                    code = "success",
                    description = "Get Permission"
                },
                data = permission.Select(p => new {
                    PermissionId = p.PermissionId,
                    PermissionName = p.PermissionName
                })
            };
            return Ok(response);
        }
    }
}
using Application.Interface;
using Domain.Models.Role;
using Domain.ViewModels.Permission;
using Microsoft.AspNetCore.Mvc;
using Application.Security;
using Eshop.Common;

namespace Eshop.Areas.Admin.Controllers.Permission
{
    public class PermissionController : AdminBaseController
    {
        private IPermissionService _permissionService;
        private ILoggerService _loggerService;

        public PermissionController(IPermissionService permissionService, ILoggerService loggerService)
        {
            _permissionService = permissionService;
            _loggerService = loggerService;
        }

        [Route("Roles")]
        [CheckPermission(Permissions.RoleManagement)]
        public async Task<IActionResult> Index()
        {
            return View(await _permissionService.GetAllRoles());
        }

        #region Add

        [Route("AddRole")]
        //[CheckPermission(Permissions.AddRole)]
        public async Task<IActionResult> AddRole()
        {
            ViewData["Permission"] = await _permissionService.GetAllPermission();
            return View();
        }

        [Route("AddRole")]
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel model, List<int> selectedPermission)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var res = await _permissionService.AddRole(model);
            var rer2 = await _permissionService.AddPermissionRole(res, selectedPermission);
            await _loggerService.AddLog(res, User.GetUserId(), "افزودن نقش");
            return Redirect("/Admin/Roles");
        }
        #endregion

        [CheckPermission(Permissions.EditRole)]
        [Route("EditRole/{id}")]
        public async Task<IActionResult> EditRole(int id)
        {
            var role = await _permissionService.GetRoleById(id);
            ViewData["Permission"] = await _permissionService.GetAllPermission();
            ViewData["SelectedPermission"] = await _permissionService.GetPermissionRoles(id);
            return View(new EditRoleViwModel(){Id = role.Id,Title = role.RoleTitle});
        }


        [Route("EditRole/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViwModel model, List<int> selectedPermission)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var res = await _permissionService.UpdateRole(model);

            var res2 = await _permissionService.EditPermissionRole(model.Id, selectedPermission);
            await _loggerService.AddLog((int)model.Id, User.GetUserId(), "ویرایش نقش");
            return Redirect("/Admin/Roles");
        }

        [CheckPermission(Permissions.DeleteRole)]
        [Route("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var Role = await _permissionService.GetRoleById(id);
            var res = await _permissionService.DeleteRole(Role);
            await _loggerService.AddLog(id, User.GetUserId(), "حذف نقش");
            if (res)
            {
                return Redirect("/Admin/Roles");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

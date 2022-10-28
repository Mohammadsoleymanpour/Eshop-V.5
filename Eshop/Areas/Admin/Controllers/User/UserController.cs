using Application.Interface;
using Application.Security;
using Domain.Interfaces;
using Domain.Models.Role;
using Domain.ViewModels.User;
using Eshop.Common;
using Microsoft.AspNetCore.Mvc;


namespace Eshop.Areas.Admin.Controllers.User
{

    public class UserController : AdminBaseController
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        private ILoggerService _loggerService;

        public UserController(IUserService userService, IPermissionService permissionService, ILoggerService loggerService)
        {
            _userService = userService;
            _permissionService = permissionService;
            _loggerService = loggerService;
        }
        [CheckPermission(Permissions.UserManagement)]
        [Route("GetUsers")]
        public IActionResult GetUsers(string filteremail = "", string filterphoneNumber = "", int pageId = 1)
        {

            return View(_userService.GetUserForAdmin(filteremail, filterphoneNumber, pageId));
        }
        [CheckPermission(Permissions.AddUser)]
        [Route("AddUser")]
        public IActionResult AddUser()
        {
            return View();
        }
        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser(UserFroAdmin user)
        {


            if (!ModelState.IsValid)
            {
                return View(user);
            }

            Domain.Models.UserAgg.User IsExist = _userService.GetUserByEmail(user.Email);
            if (IsExist != null)
            {
                ViewBag.Error = true;
                return View(user);
            }
            int id = _userService.AddUserFromAdmin(user);
           await _loggerService.AddLog(id, User.GetUserId(), "افزودن کاربر");
            return Redirect("/Admin/GetUsers");
        }
        [Route("EditUser/{id}")]
        [CheckPermission(Permissions.EditUser)]
        public IActionResult EditUser(int id)
        {
            var user = _userService.EditUserFromAdmin(id);
            if (user == null)
            {
                return Redirect("/Admin/GetUsers");
            }
            return View(user);
        }
        [Route("EditUser/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserFromAdmin user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _userService.UpdateUserFromAdmin(user);
           await _loggerService.AddLog(user.Id, User.GetUserId(), "ویرایش کاربر");
            return Redirect("/Admin/GetUsers");
        }


        [Route("DeleteUser/{id}")]
        [CheckPermission(Permissions.DeleteUser)]
        public IActionResult DeleteUser(int id)
        {
            var user = _userService.EditUserFromAdmin(id);
            if (user == null)
            {
                return Redirect("/Admin/GetUsers");
            }
            return View(user);
        }
        [Route("DeleteUser/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(EditUserFromAdmin user)
        {


            _userService.DeleteUserFromAdmin(user);
            await _loggerService.AddLog(user.Id, User.GetUserId(), "حذف کاربر");
            return Redirect("/Admin/GetUsers");
        }

        [Route("DetailUser/{id}")]
        [CheckPermission(Permissions.EditUser)]
        public IActionResult DetailUser(int id)
        {
            var user = _userService.EditUserFromAdmin(id);
            if (user == null)
            {
                return Redirect("/Admin/GetUsers");
            }
            return View(user);
        }


        [Route("UserRoles/{id}")]
        [CheckPermission(Permissions.UserRoles)]
        public async Task<IActionResult> UserRoles(int id)
        {
            return View(await _permissionService.GetRolesByUserId(id));
        }

        [Route("AddUserRoles/{id}")]
        [CheckPermission(Permissions.AddUserRole)]
        public async Task<IActionResult> AddUserRoles(int id)
        {
            ViewData["Permission"] = await _permissionService.GetAllPermission();
            ViewBag.userId = id;
            return View(await _permissionService.GetAllRoles());
        }

        [Route("AddUserRoles/{id}")]
        [HttpPost]
        public async Task<IActionResult> AddUserRoles(int userId, List<int> selectedPermission)
        {
            if (selectedPermission == null)
            {
                return BadRequest();

            }

            var res = await _permissionService.AddRoleUser(selectedPermission, userId);
            await _loggerService.AddLog(res, User.GetUserId(), "افزودن نقش");
            return Redirect("/Admin/UserRoles/" + userId);
        }


        [Route("DeleteUserRole/{id}")]
        [CheckPermission(Permissions.DeleteUserRole)]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            var res = await _permissionService.DeleteUserRole(id);
            if (res != true)
            {
                return BadRequest();
            }
            await _loggerService.AddLog(id, User.GetUserId(), "حذف دسترسی");
            return Redirect("/Admin/GetUsers");
        }

    }
}

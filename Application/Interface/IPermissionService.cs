using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Role;
using Domain.Models.UserAgg;
using Domain.ViewModels.Permission;

namespace Application.Interface
{
    public interface IPermissionService
    {
        #region Role

        Task<int> AddRole(RoleViewModel model);
        Task<List<Role>> GetAllRoles();
        Task<bool> DeleteRole(Role model);
        Task<bool> UpdateRole(EditRoleViwModel model);
        Task<Role> GetRoleById(int id);

        Task<int> AddRoleUser(List<int> roleId, int userId);
        Task<bool> EditRoleUser(List<int> roleId, int userId);
        Task<Tuple<List<UserRoles>, User>> GetRolesByUserId(int userId);
        Task<bool> DeleteUserRole(int UserRole);
        Task<UserRoles> GetUserRolesById(int userRoleId);
        #endregion

        #region Permission

        Task<int> AddPermissionRole(int roleId, List<int> selectedPermission);

        Task<List<int>> GetPermissionRoles(int id);
        Task<bool> EditPermissionRole(int roleId, List<int> selectedPermission);

        Task<bool> CheckPermission(int permissionId, int userId);
        Task<List<Permission>> GetAllPermission();

        #endregion
    }
}

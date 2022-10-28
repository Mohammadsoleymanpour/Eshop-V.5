using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Domain.Interfaces;
using Domain.Models.Role;
using Domain.Models.UserAgg;
using Domain.ViewModels.Permission;

namespace Application.Services
{
    public class PermissionService:IPermissionService
    {
        private IPermissionRoleRepository _permissionRoleRepository;

        public PermissionService(IPermissionRoleRepository permissionRoleRepository)
        {
            _permissionRoleRepository = permissionRoleRepository;
        }
        public async Task<int> AddRole(RoleViewModel model)
        {
            var AddRole = new Role()
            {
                CreatDate = DateTime.Now,
                IsDelete = false,
                RoleTitle = model.Title,
            };
            return await _permissionRoleRepository.AddRole(AddRole);
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _permissionRoleRepository.GetAllRoles();
        }

        public async Task<bool> DeleteRole(Role model)
        {
            return await _permissionRoleRepository.DeleteRole(model);
        }

        public async Task<bool> UpdateRole(EditRoleViwModel model)
        {
            var role = await GetRoleById(model.Id);
            if (role == null)
            {
                return false;

            }

            role.RoleTitle=model.Title;
            
            return await _permissionRoleRepository.UpdateRole(role);
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _permissionRoleRepository.GetRoleById(id);
        }

        public async Task<int> AddRoleUser(List<int> roleId, int userId)
        {
            return await _permissionRoleRepository.AddRoleUser(roleId, userId);
        }

        public async Task<bool> EditRoleUser(List<int> roleId, int userId)
        {
            return await _permissionRoleRepository.EditRoleUser(roleId, userId);
        }

        public async Task<Tuple<List<UserRoles>, User>> GetRolesByUserId(int userId)
        {
            return await _permissionRoleRepository.GetRolesByUserId(userId);
        }

        public async Task<bool> DeleteUserRole(int UserRole)
        {
            return await _permissionRoleRepository.DeleteUserRole(UserRole);
        }

        public async Task<UserRoles> GetUserRolesById(int userRoleId)
        {
            return await _permissionRoleRepository.GetUserRolesById(userRoleId);
        }

        public async Task<int> AddPermissionRole(int roleId, List<int> selectedPermission)
        {
            return await _permissionRoleRepository.AddPermissionRole(roleId, selectedPermission);
        }

        public async Task<List<int>> GetPermissionRoles(int id)
        {
            return await _permissionRoleRepository.GetPermissionRoles(id);
        }

        public async Task<bool> EditPermissionRole(int roleId, List<int> selectedPermission)
        {
            return await _permissionRoleRepository.EditPermissionRole(roleId, selectedPermission);
        }

        public async Task<bool> CheckPermission(int permissionId, int userId)
        {
            return await _permissionRoleRepository.CheckPermission(permissionId, userId);
        }

        public async Task<List<Permission>> GetAllPermission()
        {
            return await _permissionRoleRepository.GetAllPermission();
        }
    }
}

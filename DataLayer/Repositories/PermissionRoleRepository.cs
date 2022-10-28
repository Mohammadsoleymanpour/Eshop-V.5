using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext;
using Domain.Interfaces;
using Domain.Models.Role;
using Domain.Models.UserAgg;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class PermissionRoleRepository : IPermissionRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddRole(Role model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model.Id;

        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<bool> DeleteRole(Role model)
        {
            //var role = await _context.Roles.FindAsync(model);
            model.IsDelete = true;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {

                return false;
            }
            return true;
        }

        public async Task<bool> UpdateRole(Role model)
        {
            _context.Roles.Update(model);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {

                return false;
            }
            return true;
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> AddRoleUser(List<int> roleId, int userId)
        {
            foreach (var role in roleId)
            {
                var userRole = new UserRoles()
                {
                    UserId = userId,
                    CreatDate = DateTime.Now,
                    RoleId = role,
                    IsDelete = false,
                };
                _context.Add(userRole);
                await _context.SaveChangesAsync();
            }
            return userId;
        }

        public async Task<bool> EditRoleUser(List<int> roleId, int userId)
        {
            foreach (var role in roleId)
            {
                _context.UserRoles.Where(ur => ur.UserId == userId).ToList().ForEach(c => _context.UserRoles.Remove(c));
                await _context.SaveChangesAsync();
            }

            try
            {
                // await AddRoleUser(roleId, userId);

                return true;
            }
            catch
            {

                return false;
            }
        }

        public async Task<Tuple<List<UserRoles>, User>> GetRolesByUserId(int userId)
        {
            var res = await _context.UserRoles.Include(c => c.Role).Where(c => c.UserId == userId).ToListAsync();
            var user = _context.UserRoles.Include(c => c.User).FirstOrDefault(c => c.UserId == userId).User;
            return Tuple.Create(res, user);
        }

        public async Task<bool> DeleteUserRole(int UserRole)
        {
            var userRole = await GetUserRolesById(UserRole);

            userRole.IsDelete = true;
            _context.UserRoles.Update(userRole);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserRoles> GetUserRolesById(int userRoleId)
        {
            return await _context.UserRoles.FindAsync(userRoleId);
        }

        public async Task<int> AddPermissionRole(int roleId, List<int> selectedPermission)
        {
            foreach (var item in selectedPermission)
            {
                await _context.RolePermissions.AddAsync(
                     new RolePermission()
                     {
                         CreatDate = DateTime.Now,
                         IsDelete = false,
                         PermissionId = item,
                         RoleId = roleId,

                     }
                 );
                await _context.SaveChangesAsync();
            }

            return roleId;
        }

        public async Task<List<int>> GetPermissionRoles(int id)
        {
            return await _context.RolePermissions.Where(c => c.RoleId == id).Select(c => c.PermissionId).ToListAsync();
        }

        public async Task<bool> EditPermissionRole(int roleId, List<int> selectedPermission)
        {

            _context.RolePermissions.Where(c => c.RoleId == roleId).ToList().ForEach(c => _context.RemoveRange(c));
            await AddPermissionRole(roleId, selectedPermission);
            return true;

        }

        public async Task<bool> CheckPermission(int permissionId, int userId)
        {
            var User = _context.Users.FirstOrDefault(c => c.Id == userId);

            List<int>? UserRole = _context.UserRoles.Where(c => c.UserId == userId).Select(c => c.RoleId).ToList();

            if (!UserRole.Any())
            {
                return false;
            }

            List<int>? RolePermission = _context.RolePermissions.Where(c => c.PermissionId == permissionId)
                .Select(c => c.RoleId).ToList();
            if (RolePermission == null)
            {
                return false;
            }

            var res = RolePermission.Any(c => UserRole.Contains(c));
            return res;
        }

        public async Task<List<Permission>> GetAllPermission()
        {
            var res = await _context.Permissions.ToListAsync();
            return res;
        }
    }
}

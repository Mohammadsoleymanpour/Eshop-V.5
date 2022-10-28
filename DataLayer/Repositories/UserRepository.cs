using DataLayer.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using IQueryable = System.Linq.IQueryable;
using Domain.ViewModels.User;
using Domain.Interfaces;
using Domain.Models.UserAgg;

namespace DataLayer.Repositories
{


    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext contex)
        {
            _context = contex;
        }
        public int AddUser(User user)
        {
            _context.Users.Add(user);
            Save();
            return user.Id;
        }

        public void DeleteUser(User user)
        {
            user.IsDelete = true;
            user.Status = Status.Ban;
            EditUser(user);
            Save();
        }

        public bool DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                DeleteUser(user);
                return true;
            }

            return false;
        }

        public void EditUser(User user)
        {
            _context.Users.Update(user);
            Save();
        }

        public bool EmailIsExist(string email)
        {

            return _context.Users.Any(e => e.Email == email);

        }

        public List<User> GetAllUsers()
        {
            return _context.Users.AsNoTracking().ToList();
        }

        public List<GetUserForAdminViewMdoel> GetUsersForAdmin(string emailFilter, string phoneNumberFilter, int pageId)
        {
            int take = 8;
            int skip = (pageId - 1) * take;
            IQueryable<User> res = _context.Users;
            res = res.Where(u => u.Email.Contains(emailFilter) || u.PhoneNumber.Contains(phoneNumberFilter))
                .Take(take)
                .Skip(skip);
            List<GetUserForAdminViewMdoel> result = new List<GetUserForAdminViewMdoel>();
           return result = res.Select(c => new GetUserForAdminViewMdoel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Gender = c.Gender,
                IsActive = c.Status,
                IsAdmin = c.IsAdmin


            }).ToList();
            
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(c=>c.Email==email);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public User? GetUserById(int userId)
        {
            return _context.Users.SingleOrDefault(user => user.Id == userId);
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
#pragma warning disable CS8603
            return _context.Users.SingleOrDefault(c => c.Email == email && c.Password == password);
#pragma warning restore CS8603
        }

        public bool ComparePassWord(string email, string passWord)
        {
            var user = GetUserByEmail(email);
            return user.Password == passWord;
        }

        public User GetUserByActiveCode(string code)
        {
            return _context.Users.FirstOrDefault(c => c.ActiveCode == code);
        }

        public User? GstUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }
    }
}

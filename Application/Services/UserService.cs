using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using DataLayer.Repositories;
using Domain.Models.Enums;
using Domain.ViewModels.User;
using Application.Security;
using Domain.Interfaces;
using Domain.Models.UserAgg;
using Application.Convertor;

namespace Application.Services
{
    public class UserService:IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    


        public User RegisterUser(UserViewModel userViewModel)
        {
            


            User user = new User()
            {
                Email = userViewModel.Email,
                Password = PasswordHelper.EncodePasswordMd5(userViewModel.Password),
                Gender = userViewModel.Gender,
                PhoneNumber = userViewModel.PhoneNumber,
                BirthDate = userViewModel.BirthDay,
                CreatDate = DateTime.Now,
                Status = Status.NotActive,
                ActiveCode = NameGenerator.GeneratorUniqCode(),
                IsAdmin = false,
                IsDelete = false,
            };
            _userRepository.AddUser(user);
            return user;
        }

        public List<GetUserForAdminViewMdoel> GetUserForAdmin(string emailFilter = "", string phoneNumber = "", int pageId = 1)
        {
            return _userRepository.GetUsersForAdmin(emailFilter, phoneNumber, pageId);
        }

        public User LoginUser(LoginViewModel login)
        {
            string email = FixedText.FixEmail(login.Email);
            string hashPassword=PasswordHelper.EncodePasswordMd5(login.Password);

           return _userRepository.GetUserByEmailAndPassword(email, hashPassword);
        }

        public bool UserIsExist(UserViewModel userViewModel)
        {
            var user = _userRepository.GetUserByEmail(userViewModel.Email);
            if (user!=null)
            {
                return true;
            }
            
            return false;
           
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public bool UserIsExist(User user)
        {
            var Getuser = _userRepository.GetUserByEmail(user.Email);
            if (user != null)
            {
                return true;
            }

            return false;
        }

        public void UpdateUserFromAdmin(EditUserFromAdmin user)
        {
            string pass = "";
            var getuser = _userRepository.GetUserById(user.Id);
           
            getuser.Email=user.Email;
            if (!string.IsNullOrEmpty(user.Password))
            {
                getuser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            }

            getuser.BirthDate = user.BirthDay;
            getuser.FirstName=user.FirstName;
            getuser.LastName=user.LastName;
            getuser.Gender=user.Gender;
            getuser.Status = Status.Active;
            getuser.IsAdmin=user.IsAdmin;
            getuser.PhoneNumber=user.PhoneNumber;
            getuser.IsDelete = false;

            _userRepository.EditUser(getuser);
            
        }

        public void DeleteUserFromAdmin(EditUserFromAdmin user)
        {
            var getuser = _userRepository.GetUserById(user.Id);
            _userRepository.DeleteUser(getuser.Id);
        }

        public EditUserFromAdmin EditUserFromAdmin(int id)
        {
           var user= _userRepository.GetUserById(id);
           EditUserFromAdmin edit=new EditUserFromAdmin()
           {
               Email = user.Email,
               BirthDay = user.BirthDate,
               FirstName = user.FirstName,
               Gender = user.Gender,
               Id = user.Id,
               LastName = user.LastName,
               PhoneNumber = user.PhoneNumber,
               IsAdmin = user.IsAdmin,
               Password = user.Password
           };
           return edit;
        }

        public int AddUserFromAdmin(UserFroAdmin user)
        {
            User addUser = new User()
            {
                Email = user.Email,
                Password = PasswordHelper.EncodePasswordMd5(user.Password),
                PhoneNumber = user.PhoneNumber,
                ActiveCode = NameGenerator.GeneratorUniqCode(),
                BirthDate = user.BirthDay,
                CreatDate = DateTime.Now,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin,
                Gender = user.Gender,
                IsDelete = false,
                Status = Status.Active,
            };
            _userRepository.AddUser(addUser);
            return addUser.Id;
        }

        public User GetUserByActiveCode(string code)
        {
            return _userRepository.GetUserByActiveCode(code);
        }

        public bool ResetPassword(string password,int userId)
        {
            var user = GetUserById(userId);
            if (user==null)
            {
                return false;
            }
            user.Password = PasswordHelper.EncodePasswordMd5(password);
            _userRepository.EditUser(user);
            return true;
        }

        public bool ActiveAccount(string id)
        {
            var user= _userRepository.GstUserByActiveCode(id);
            if (user==null)
            {
                return false;
            }
            user.Status=Status.Active;
            user.ActiveCode = NameGenerator.GeneratorUniqCode();
            _userRepository.EditUser(user);
            return true;
        }

        public bool ComparePassWord(string email, string passWord)
        {
            string fixedEmail = FixedText.FixEmail(email);
            string hashPassWord = PasswordHelper.EncodePasswordMd5(passWord);
            return _userRepository.ComparePassWord(fixedEmail, hashPassWord);
        }

        public EditUserStatus UpdateProfileUser(int id, UserForProfileDetailViewModel userViewModel)
        {
            var userOldInfo = _userRepository.GetUserById(id);
            if (userOldInfo==null)
            {
                return EditUserStatus.notFoundUser;
            }
            userOldInfo.PhoneNumber = userViewModel.PhoneNumber;
            userOldInfo.LastName = userViewModel.LastName;
            userOldInfo.FirstName = userViewModel.FirstName;
            userOldInfo.BirthDate = userViewModel.BirthDay;
            userOldInfo.Gender = userViewModel.Gender;
            
            _userRepository.EditUser(userOldInfo);

            return EditUserStatus.success;
        }

        public List<GetUserForAdminViewMdoel> GetUserForAdmin(int pageId = 1,string emailFilter = "" , string phoneNumber = "")
        {
            return  _userRepository.GetUsersForAdmin(emailFilter, phoneNumber, pageId);
        }
    }
}

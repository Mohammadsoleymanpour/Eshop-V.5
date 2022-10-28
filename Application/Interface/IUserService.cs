using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.UserAgg;
using Domain.ViewModels.User;

namespace Application.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// Register (Add) A New User To DataBase
        /// </summary>
        /// <param name="userViewModel">A ViewModel With All Needed Fields</param>
        /// <returns>The Newly Added User</returns>
        User RegisterUser(UserViewModel userViewModel);

        /// <summary>
        /// Get A List Of Users For Admin With Filter Search And Paging
        /// </summary>
        /// <param name="emailFilter">Filter Search Of Email</param>
        /// <param name="phoneNumber">Filter Search Of Phone Number</param>
        /// <param name="pageId">Current Page Id </param>
        /// <returns>A View Model Of Needed Info</returns>
        List<GetUserForAdminViewMdoel> GetUserForAdmin(string emailFilter = "", string phoneNumber = "", int pageId = 1);
       
        /// <summary>
        /// Get A User For Login
        /// </summary>
        /// <param name="login">A View Model With Login Needed Info</param>
        /// <returns>User That Is Gooing To Login If The Data Is Ok </returns>
        User LoginUser(LoginViewModel login);

        /// <summary>
        /// A Method To Check If User Exist
        /// </summary>
        /// <param name="userViewModel">A View Model With Needed Data</param>
        /// <returns>A Boolean That Indicate The Result </returns>
        bool UserIsExist(UserViewModel userViewModel);
       
        /// <summary>
        /// Get A User By Id
        /// </summary>
        /// <param name="id">Id Of Needed User</param>
        /// <returns>A User Model</returns>
        User GetUserById(int id);
       
        /// <summary>
        /// Get A User Using Email
        /// </summary>
        /// <param name="email">Email Of Needed User</param>
        /// <returns>A User Model</returns>
        User GetUserByEmail(string email);
        
        /// <summary>
        /// A Method To Active User Account
        /// </summary>
        /// <param name="id">A Guid In Form Of String For Active Code Of User</param>
        /// <returns>A Boolean That Indicate The Result</returns>
        bool ActiveAccount(string id);

        /// <summary>
        /// A Method That Check PassWord With Email To See If This PassWord Match DataBase
        /// </summary>
        /// <param name="email">Email Of User</param>
        /// <param name="passWord">The Comparable PassWord</param>
        /// <returns>A Boolean That Indicate The Result</returns>
        bool ComparePassWord(string email, string passWord);
       
        /// <summary>
        /// A Method To Update The User's Profile Detail
        /// </summary>
        /// <param name="id">User'sId</param>
        /// <param name="userViewModel">A View Model Of User's Profile Data To Update</param>
        /// <returns>A Enum To Indicate Result Of Update</returns>
        EditUserStatus UpdateProfileUser(int id, UserForProfileDetailViewModel userViewModel);

        /// <summary>
        /// A Method To Check If User Exist
        /// </summary>
        /// <param name="user">A User Model</param>
        /// <returns>A Boolean That Indicate The Result</returns>
        bool UserIsExist(User user);

        /// <summary>
        /// A Method To Update The User's Profile Detail From Admin
        /// </summary>
        /// <param name="user">A View Model Of User's Profile Data To Update</param>
        void UpdateUserFromAdmin(EditUserFromAdmin user);
      
        /// <summary>
        /// Delete And Ban A User From Admin
        /// </summary>
        /// <param name="user">A ViewModel Of User Data</param>
        void DeleteUserFromAdmin(EditUserFromAdmin user);
       
        /// <summary>
        /// Get A ViewModel For Edit User From Admin
        /// </summary>
        /// <param name="id">User's Id To Get</param>
        /// <returns>A ViewModel For Editeing</returns>
        EditUserFromAdmin EditUserFromAdmin(int id);
        
        /// <summary>
        /// Add A User From Admin
        /// </summary>
        /// <param name="user">A ViewModel With Needed Data</param>
        /// <returns>Id Of Newly Add User</returns>
        int AddUserFromAdmin(UserFroAdmin user);

        User GetUserByActiveCode(string code);

        bool ResetPassword(string password,int userId);
    }
}

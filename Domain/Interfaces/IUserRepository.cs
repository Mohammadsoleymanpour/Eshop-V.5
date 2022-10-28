using Domain.Models.UserAgg;
using Domain.ViewModels.User;

namespace Domain.Interfaces
{
    public interface IUserRepository

    {
        int AddUser(User user);
        User? GetUserById(int userId);
        void DeleteUser(User user);
        bool DeleteUser(int userId);
        void EditUser(User user);
        bool EmailIsExist(string email);
        User? GstUserByActiveCode(string activeCode);
        List<User> GetAllUsers();
        List<GetUserForAdminViewMdoel> GetUsersForAdmin(string emailFilter, string phoneNumberFilter, int pageId);
        User GetUserByEmail(string email);
        void Save();
        User GetUserByEmailAndPassword(string email, string password);
        bool ComparePassWord(string email, string passWord);
        User GetUserByActiveCode(string code);


    }
}

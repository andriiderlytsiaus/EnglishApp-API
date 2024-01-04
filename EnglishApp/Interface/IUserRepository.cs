using EnglishApp.Models;

namespace EnglishApp.Interface
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool CreateUser(User user);
        bool UserExists(int id);
        bool Save();
    }
}

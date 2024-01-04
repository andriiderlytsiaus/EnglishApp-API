using EnglishApp.Data;
using EnglishApp.Interface;
using EnglishApp.Models;

namespace EnglishApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EnglishAppContext _context;
        public UserRepository(EnglishAppContext context) {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(x =>x.Id == id);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >0 ? true : false;
        }

        public bool UserExists(int id)
        {
            if(_context.Users.Any(x => x.Id == id))
                return true;
            else return false;
        }
        
    }
}

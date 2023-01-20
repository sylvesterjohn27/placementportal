using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;

namespace PlacementManagement.DAL.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly PlacementManagementAppDbContext _DbContext;

        public UserRepository(PlacementManagementAppDbContext DbContext)
        {
            _DbContext= DbContext;
        }
        public void AddUser(User user)
        {
            _DbContext.Users.Add(user);   
            _DbContext.SaveChanges();
        }

        public User GetUserByUserName(string userName)
        {
            return _DbContext.Users.FirstOrDefault(x => x.UserName == userName);
        }
    }
}

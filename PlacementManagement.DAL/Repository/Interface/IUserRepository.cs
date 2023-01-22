using PlacementManagement.DAL.Models;

namespace PlacementManagement.DAL.Repository.Interface
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUserByUserName(string userName);
        User GetUserById(int id);
    }
}

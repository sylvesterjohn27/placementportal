using PlacementManagement.BAL.Models;

namespace PlacementManagement.BAL.Services.Interfaces
{
    public interface IUserServices
    {
        void AddUser(RegisterViewModel user);
        UserViewModel GetUserByUserName(string userName);
    }
}

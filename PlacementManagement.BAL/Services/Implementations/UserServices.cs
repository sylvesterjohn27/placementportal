using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;

namespace PlacementManagement.BAL.Services.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(RegisterViewModel user)
        {
            var usr = new User
            {
                AccountTypeId = user.AccountTypeId,
                Name = user.Name,
                UserName = user.UserName,
                Password = user.Password
            };
            _userRepository.AddUser(usr);
        }        

        public UserViewModel GetUserByUserName(string userName)
        {
            var userDetails = new UserViewModel();
            var user = _userRepository.GetUserByUserName(userName);
            if (user != null)
            {
                userDetails.Id = user.Id;
                userDetails.Name = user.Name;
                userDetails.UserName = user.UserName;
                userDetails.AccountTypeId = user.AccountTypeId;
            }
            return userDetails;
        }
    }
}
using System.Collections.Generic;
using UserManager.DataEntities.Models;

namespace UserManager.Services.Interface
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        User GetUserByUsername(string username);
        Dictionary<string, string> ValidateUser(User user);
        void UpdateUserGroups(string[] selectedGroups, User userToUpdate);

    }
}
